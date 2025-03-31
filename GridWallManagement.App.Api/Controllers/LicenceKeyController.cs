using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenceKeyController : ApiControllerBase
    {
        private readonly ILogger<LicenceKeyController> _logger;
        private readonly ILicenceKeyService _licenceKeyService;

        public LicenceKeyController(ILicenceKeyService licenceKeyService,
                                    IMapper mapper) : base(mapper)
        {
            _licenceKeyService = licenceKeyService;
        }

        /// <summary>
        /// Get user licences
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[SwaggerIgnore]
        [HttpGet("get-user-licenses")]
        public async Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync([FromQuery] GetUserLicensesRequest request)
            => await _licenceKeyService.GetUserLicensesAsync(request);

        /// <summary>
        /// Validate license key
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("validate-license-key")]
        public async Task<ResponseBase<bool>> ValidateLicenseKey([FromBody] ValidateLicenseKeyRequest request)
            => await _licenceKeyService.ValidateLicenseKey(request);

        /// <summary>
        /// Register user licence key 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register-user-licence")]
        public async Task<ResponseBase<UserLicenseResponse>> RegisterUserLicence([FromBody] RegisterUserLicenceRequest request)
            => await _licenceKeyService.RegisterUserLicence(request);
    }
}