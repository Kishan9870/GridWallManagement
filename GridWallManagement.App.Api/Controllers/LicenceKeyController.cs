using AutoMapper;
using GridWallManagement.App.Api.Controllers;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWall.App.Api.Controllers
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
        /// Demo
        /// </summary>
        /// <returns></returns>
        [HttpPost("Demo")]
        public async Task<ResponseBase<string>> Demo()
        {

            //var response = await _licenceKeyService.Authenticate(request, IpAddress(), cancellationToken);

            
            return null;
        }
    }
}