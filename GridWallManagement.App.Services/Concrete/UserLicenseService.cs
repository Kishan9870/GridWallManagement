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

        public async Task<PagedResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request)
        {
            try
            {
                var userLicenses = await _userLicenseRepository.GetQueryable()
                                                               .Where(x => x.IsActive)
                                                                    .Include(x => x.Renewals)
                                                               .GetPage<UserLicenseResponse, UserLicense>(request, this._mapper)
                                                               .ToListAsync();

                if (!userLicenses.Any())
                    throw new Exception("User licenses not found.");

                var userLicensesCounts = await _userLicenseRepository.GetQueryable().Where(x => x.IsActive).CountAsync();

                var responses = _mapper.Map<List<UserLicenseResponse>>(userLicenses);

                return new PagedResponseBase<List<UserLicenseResponse>>(responses, request.PageNumber, request.PageSize, (int)userLicensesCounts);
            }
            catch (Exception ex)
            {
                var result = new PagedResponseBase<List<UserLicenseResponse>>(null,
                                                                              request.PageNumber,
                                                                              request.PageSize,
                                                                              0)
                { ResponseStatusCode = System.Net.HttpStatusCode.UnprocessableContent };
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
