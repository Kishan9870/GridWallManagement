using System.Net;
using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GridWallManagement.App.Services.Concrete
{
    public class RolesService : ServiceBase, IRolesService
    {
        private readonly IGenericRepository<Roles> _rolesRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RolesService(IGenericRepository<Roles> rolesRepository,
                            IConfiguration config,
                            IMapper mapper,
                            IUserContext userContext) : base(mapper, userContext)
        {
            _rolesRepository = rolesRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<PagedResponseBase<List<RolesResponse>>> GetAsync(GetRolesRequest request)
        {
            try
            {
                var query = _rolesRepository.GetQueryable()
                                                  .Where(x => x.IsActive);
                //                                  .GetPage<RolesResponse, Roles>(request, this._mapper)
                //.ToListAsync();

                var totalCount = await query.CountAsync();
                var pagedQuery = query.GetPage<RolesResponse, Roles>(request, _mapper);
                var roles = await pagedQuery.ToListAsync();
                var mappedRoles = _mapper.Map<List<RolesResponse>>(roles);

                return new PagedResponseBase<List<RolesResponse>>(mappedRoles, request.PageNumber, request.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                var result = new PagedResponseBase<List<RolesResponse>>(null, request.PageNumber, request.PageSize, 0);
                result.ResponseStatusCode = HttpStatusCode.UnprocessableContent;
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
