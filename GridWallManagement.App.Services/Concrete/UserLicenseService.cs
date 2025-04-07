using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.UserLicense;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class UserLicenseService : ServiceBase, IUserLicenseService
    {
        private readonly IGenericRepository<UserLicense> _userLicenseRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserLicenseService(IGenericRepository<UserLicense> userLicenseRepository,
                                  IConfiguration config,
                                  IMapper mapper,
                                  IUserContext userContext) : base(mapper, userContext)
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
    }
}
