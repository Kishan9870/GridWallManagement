using AutoMapper;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilityController : ApiControllerBase
    {
        private readonly ILogger<UtilityController> _logger;
        private readonly IUtilityService _utilityService;

        public UtilityController(IUtilityService utilityService,
                                 IMapper mapper) : base(mapper)
        {
            _utilityService = utilityService;
        }

        /// <summary>
        /// Get syncerdb connection
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-syncerdb-connection")]
        public async Task<ResponseBase<string>> GetSyncerDBConnection()
            => await _utilityService.GetSyncerDBConnection();
    }
}