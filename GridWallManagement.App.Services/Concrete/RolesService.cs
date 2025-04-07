using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Models.Response.UserLicense;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Service.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class RolesService : ServiceBase, IRolesService
    {
        private readonly IGenericRepository<Roles> _rolesRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RolesService(IGenericRepository<Roles> rolesRepository,
                            IConfiguration config,
                            IMapper mapper) : base(mapper)
        {
            _rolesRepository = rolesRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<RolesResponse>>> GetAsync(GetRolesRequest request)
        {
            try
            {
                var roles = await _rolesRepository.GetQueryable()
                                                         .Where(x => x.IsActive)
                                                         .GetPage<RolesResponse, Roles>(request, this._mapper)
                                                         .ToListAsync();

                return new ResponseBase<List<RolesResponse>>(_mapper.Map<List<RolesResponse>>(roles));
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<List<RolesResponse>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
