using AutoMapper;
using GridWallManagement.App.Models.Request.Account;
using GridWallManagement.App.Models.Request.User;
using GridWallManagement.App.Models.Response.Account;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ApiControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService,
                                 IMapper mapper) : base(mapper)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<ActionResult<ResponseBase<AuthenticateResponse>>> Authenticate([FromBody] AuthenticateRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _accountService.Authenticate(request, IpAddress(), cancellationToken);

            if (response.IsSuccess)
                SetTokenCookie(response.Result?.RefreshToken);

            return response;
        }


        #region Private Methods
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        #endregion
    }
}