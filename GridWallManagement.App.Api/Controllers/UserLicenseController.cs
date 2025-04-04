using AutoMapper;
using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.UserLicense;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLicenseController : ApiControllerBase
    {
        private readonly ILogger<UserLicenseController> _logger;
        private readonly IUserLicenseService _userLicenseService;

        public UserLicenseController(IUserLicenseService userLicenseService,
                                     IMapper mapper) : base(mapper)
        {
            _userLicenseService = userLicenseService;
        }

        /// <summary>
        /// Get user licenses
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-user-licenses")]
        public async Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync([FromQuery] GetUserLicensesRequest request)
            => await _userLicenseService.GetUserLicensesAsync(request);
    }
}