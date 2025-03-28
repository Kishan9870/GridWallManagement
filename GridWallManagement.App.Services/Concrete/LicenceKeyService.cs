using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Service.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class LicenceKeyService : ServiceBase, ILicenceKeyService
    {
        private readonly IGenericRepository<UserLicense> _userLicenseRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LicenceKeyService(IGenericRepository<UserLicense> userLicenseRepository,
                                 IConfiguration config,
                                 IMapper mapper) : base(mapper)
        {
            _userLicenseRepository = userLicenseRepository;
            _config = config;
            _mapper = mapper;
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
    }
}
