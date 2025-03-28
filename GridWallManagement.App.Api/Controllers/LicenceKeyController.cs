using AutoMapper;
using GridWallManagement.App.Api.Controllers;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("get-user-licenses")]
        public async Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync([FromQuery] GetUserLicensesRequest request)
            => await _licenceKeyService.GetUserLicensesAsync(request);
    }
}