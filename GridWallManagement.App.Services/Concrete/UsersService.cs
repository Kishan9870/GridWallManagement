using System.Net;
using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.User;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class UsersService : ServiceBase, IUsersService
    {
        private readonly IGenericRepository<Users> _usersRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UsersService(IGenericRepository<Users> usersRepository,
                            IConfiguration config,
                            IMapper mapper,
                            IUserContext userContext) : base(mapper, userContext)
        {
            _usersRepository = usersRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<PagedResponseBase<List<UserResponse>>> GetAsync(GetUsersRequest request)
        {
            try
            {
                var query = _usersRepository.GetQueryable()
                                                  .Where(x => x.IsActive);
                //.GetPage<UserResponse, Users>(request, this._mapper)
                //.ToListAsync();

                var totalCount = await query.CountAsync();
                var pagedQuery = query.GetPage<UserResponse, Users>(request, _mapper);
                var users = await pagedQuery.ToListAsync();

                var mappedUsers = _mapper.Map<List<UserResponse>>(users);

                return new PagedResponseBase<List<UserResponse>>(mappedUsers, request.PageNumber, request.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                var result = new PagedResponseBase<List<UserResponse>>(null, request.PageNumber, request.PageSize, 0);
                result.ResponseStatusCode = HttpStatusCode.UnprocessableContent;
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
