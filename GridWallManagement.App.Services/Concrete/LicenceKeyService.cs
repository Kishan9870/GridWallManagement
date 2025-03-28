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

        public LicenceKeyService(IGenericRepository<UserLicense> userLicenseRepository,
                                 IConfiguration config,
                                 IMapper mapper) : base(mapper)
        {
            _userLicenseRepository = userLicenseRepository;
            _config = config;
            _mapper = mapper;
            _encryptionKey = _config.GetSection("EncryptionKey").Value;
        }

        public async Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request)
        {
            try
            {
                var userLicenses = await _userLicenseRepository.GetQueryable()
                                                               .Where(x => x.IsActive)
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
                                                              .Where(x => x.LicenseKey.Equals(request.Key)
                                                                       && x.IsActive)
                                                              .FirstOrDefaultAsync();

                if (userLicense != null)
                    throw new Exception("Licence key already exist with another user.");
                

                if (!request.Key.Contains(":"))
                    throw new FormatException("License key format is incorrect.");

                string[] keyParts = request.Key.Split(':');

                if (keyParts.Length < 2 || string.IsNullOrEmpty(keyParts[1]))
                    throw new FormatException("Invalid license key format.");

                string encryptedPart = keyParts[1];
                string decryptedData = Decrypt(encryptedPart);

                if (!DateTime.TryParse(decryptedData, out DateTime expiryDate))
                    throw new Exception("Invalid license key.");

                return new ResponseBase<bool>(true);
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<bool>(false) { ResponseStatusCode = System.Net.HttpStatusCode.BadRequest };
                result.AddExceptionLog(ex);
                return result;
            }
        }

        #region Private Method
        public string Decrypt(string encryptedText)
        {
            try
            {
                // Normalize Base64 string
                encryptedText = encryptedText.Replace("-", "+").Replace("_", "/").Replace("?", ":");
                while (encryptedText.Length % 4 != 0) encryptedText += "=";

                byte[] fullCipher = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = Convert.FromBase64String(_encryptionKey); 

                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    byte[] iv = new byte[aes.BlockSize / 8];
                    byte[] cipherText = new byte[fullCipher.Length - iv.Length];

                    Array.Copy(fullCipher, iv, iv.Length);
                    Array.Copy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(cipherText))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Decryption failed.", ex);
            }
        }

        #endregion

    }
}
