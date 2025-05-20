using GridWallManagement.App.Models.Request.Logs;
using GridWallManagement.App.Models.Response.Logs;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface ILogsService
    {
        Task<PagedResponseBase<List<ExceptionLogResponse>>> GetExceptionLogAsync(GetExceptionLogRequest request);

        Task<PagedResponseBase<List<ApplicationUserLogResponse>>> GetApplicationUserLogAsync(GetApplicationUserLogRequest request);
    }
}
