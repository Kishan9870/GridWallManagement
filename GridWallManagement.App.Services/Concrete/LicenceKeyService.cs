using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Service.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace GridWallManagement.App.Services.Concrete
{
    public class LicenceKeyService : ServiceBase, ILicenceKeyService
    {
        private readonly IGenericRepository<UserLicense> _userLicenseRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private string _encryptionKey;
        private string _companyName;

        public LicenceKeyService(IGenericRepository<UserLicense> userLicenseRepository,
                                 IConfiguration config,
                                 IMapper mapper) : base(mapper)
        {
            _userLicenseRepository = userLicenseRepository;
            _config = config;
            _mapper = mapper;
            _encryptionKey = _config.GetSection("Encryption:Key").Value;
            _companyName = _config.GetSection("Encryption:Company").Value;
        }

        public async Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request)
        {
            try
            {
                var userLicenses = await _userLicenseRepository.GetQueryable()
                                                               .Where(x => x.IsActive)
                                                                    .Include(x => x.Renewals)
                                                               .GetPage<UserLicenseResponse, UserLicense>(request, this._mapper)
                                                               .ToListAsync();

                return new ResponseBase<List<UserLicenseResponse>>(_mapper.Map<List<UserLicenseResponse>>(userLicenses));
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<List<UserLicenseResponse>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }

        public async Task<ResponseBase<bool>> ValidateLicenseKey(ValidateLicenseKeyRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Key))
                    throw new KeyNotFoundException("License key is missing or invalid.");

                var userLicense = await _userLicenseRepository.GetQueryable()
                                                              .Where(x => x.LicenseKey == request.Key && x.IsActive)
                                                              .FirstOrDefaultAsync();

                if (userLicense != null)
                    throw new Exception("License key already used.");

                string decryptedData = Decrypt(request.Key);

                if (string.IsNullOrEmpty(decryptedData) || !decryptedData.StartsWith(_companyName))
                    throw new FormatException("Invalid license format or company mismatch.");

                string[] parts = decryptedData.Split('-');
                if (parts.Length < 2 || !parts[1].EndsWith("DAYS"))
                    throw new FormatException("License does not contain a valid duration.");

                if (!int.TryParse(parts[1].Replace("DAYS", ""), out int days))
                    throw new FormatException("Invalid license duration format.");

                return new ResponseBase<bool>(true);
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<bool>(false) { ResponseStatusCode = System.Net.HttpStatusCode.BadRequest };
                result.AddExceptionLog(ex);
                return result;
            }
        }

        public async Task<ResponseBase<UserLicenseResponse>> RegisterUserLicence(RegisterUserLicenceRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Key))
                    throw new KeyNotFoundException("License key is missing or invalid.");

                var existingLicense = await _userLicenseRepository.GetQueryable()
                                                                  .Where(x => x.LicenseKey == request.Key && x.IsActive)
                                                                  .FirstOrDefaultAsync();

                if (existingLicense != null)
                    throw new Exception("License key already used.");

                string decryptedData = Decrypt(request.Key);

                if (string.IsNullOrEmpty(decryptedData) || !decryptedData.StartsWith(_companyName))
                    throw new FormatException("Invalid license format or company mismatch.");

                string[] parts = decryptedData.Split('-');
                if (parts.Length < 2 || !parts[1].EndsWith("DAYS"))
                    throw new FormatException("License does not contain a valid duration.");

                if (!int.TryParse(parts[1].Replace("DAYS", ""), out int days))
                    throw new FormatException("Invalid license duration format.");

                var userLicense = new UserLicense()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    PublicIPAddress = request.PublicIpAddress,
                    LocalIPAddress = request.LocalIpAddress,
                    MacAddress = request.MACAddress,
                    LicenseKey = request.Key,
                    RegisteredTime = request.RegisteredTime,
                    ExpirationTime = request.RegisteredTime.AddDays(days),
                    LicenseDuration = days,
                    DeviceInfo = request.DeviceInfo
                };

                var createdLicense = await _userLicenseRepository.AddAsync(userLicense);

                return new ResponseBase<UserLicenseResponse>(_mapper.Map<UserLicenseResponse>(createdLicense));
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<UserLicenseResponse>(null) { ResponseStatusCode = System.Net.HttpStatusCode.BadRequest };
                result.AddExceptionLog(ex);
                return result;
            }
        }

        #region Private Method
        private string Decrypt(string encryptedText)
        {
            byte[] keyBytes = Convert.FromBase64String(_encryptionKey);
            byte[] encryptedBytes = Convert.FromBase64String(AdjustBase64String(encryptedText));

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private string AdjustBase64String(string input)
        {
            input = input.Replace("-", "+").Replace("_", "/");
            while (input.Length % 4 != 0) input += "=";
            return input;
        }

        #endregion

    }
}
