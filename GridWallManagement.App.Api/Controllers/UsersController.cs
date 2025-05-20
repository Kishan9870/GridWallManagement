using AutoMapper;
using GridWallManagement.App.Models.Request.User;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService,
                               IMapper mapper) : base(mapper)
        {
            _usersService = usersService;
        }


        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-async")]
        public async Task<PagedResponseBase<List<UserResponse>>> GetAsync([FromQuery] GetUsersRequest request)
            => await _usersService.GetAsync(request);
    }
}