using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.User;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Service.Common;
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
                            IMapper mapper) : base(mapper)
        {
            _usersRepository = usersRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<UserResponse>>> GetAsync(GetUsersRequest request)
        {
            try
            {
                var users = await _usersRepository.GetQueryable()
                                                  .Where(x => x.IsActive)
                                                  .GetPage<UserResponse, Users>(request, this._mapper)
                                                  .ToListAsync();

                return new ResponseBase<List<UserResponse>>(_mapper.Map<List<UserResponse>>(users));
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<List<UserResponse>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
