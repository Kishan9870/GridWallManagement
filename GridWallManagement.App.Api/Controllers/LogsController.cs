using AutoMapper;
using GridWallManagement.App.Models.Request.Logs;
using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Response.Logs;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using GridWallManagement.App.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ApiControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        private readonly ILogsService _logsService;

        public LogsController(ILogsService logsService, IMapper mapper) : base(mapper)
        {
            _logsService = logsService;
        }


        /// <summary>
        /// Get Exception Log async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-exceptionLog-async")]
        public async Task<PagedResponseBase<List<ExceptionLogResponse>>> GetExceptionLogAsync([FromQuery] GetExceptionLogRequest request)
            => await _logsService.GetExceptionLogAsync(request);


        /// <summary>
        /// Get application user log async
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-applicationUserLog-async")]
        public async Task<PagedResponseBase<List<ApplicationUserLogResponse>>> GetApplicationUserLogAsync([FromQuery] GetApplicationUserLogRequest request)
            => await _logsService.GetApplicationUserLogAsync(request);
    }
}
