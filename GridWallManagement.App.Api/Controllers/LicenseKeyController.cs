using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.LicenseKey;
using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.LicenseKey;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseKeyController : ApiControllerBase
    {
        private readonly ILogger<LicenseKeyController> _logger;
        private readonly ILicenseKeyService _licenseKeyService;

        public LicenseKeyController(ILicenseKeyService licenseKeyService,
                                    IMapper mapper) : base(mapper)
        {
            _licenseKeyService = licenseKeyService;
        }

        /// <summary>
        /// Generate new licence key
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("generate-license-key")]
        public async Task<ResponseBase<GenerateLicenseKeyResponse>> GenerateLicenseKeyAsync([FromQuery] GenerateLicenseKeyRequest request)
            => await _licenseKeyService.GenerateLicenseKeyAsync(request);

        /// <summary>
        /// Validate license key
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("validate-license-key")]
        public async Task<ResponseBase<bool>> ValidateLicenseKey([FromBody] ValidateLicenseKeyRequest request)
            => await _licenseKeyService.ValidateLicenseKey(request);

        /// <summary>
        /// Register user license key 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register-user-license")]
        public async Task<ResponseBase<bool>> RegisterUserLicense([FromBody] RegisterUserLicenseRequest request)
            => await _licenseKeyService.RegisterUserLicense(request);
    }
}