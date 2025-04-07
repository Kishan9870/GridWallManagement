using AutoMapper;
using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Models.Response.UserLicense;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ApiControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService,
                               IMapper mapper) : base(mapper)
        {
            _rolesService = rolesService;
        }

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-user-licenses")]
        public async Task<ResponseBase<List<RolesResponse>>> GetAsync([FromQuery] GetRolesRequest request)
            => await _rolesService.GetAsync(request);
    }
}