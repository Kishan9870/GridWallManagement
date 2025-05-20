using System.Net;
using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.Logs;
using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Response.Logs;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Repository.Common;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class LogsService : ServiceBase, ILogsService
    {
        private readonly IGenericRepository<ExceptionLog> _exceptionLogRepository;
        private readonly IGenericRepository<ApplicationUserLog> _applicationUserLogRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LogsService(IGenericRepository<ExceptionLog> exceptionLogRepository,
                           IGenericRepository<ApplicationUserLog> applicationUserLogRepository,
                           IConfiguration config,
                           IMapper mapper,
                           IUserContext userContext) : base(mapper, userContext)
        {
            _exceptionLogRepository = exceptionLogRepository;
            _applicationUserLogRepository = applicationUserLogRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<PagedResponseBase<List<ExceptionLogResponse>>> GetExceptionLogAsync(GetExceptionLogRequest request)
        {
            try
            {
                var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);


                var query = _exceptionLogRepository.GetQueryable()
                                                   .Where(x => x.Timestamp >= sevenDaysAgo); 


                var totalCount = await query.CountAsync();
                var pagedQuery = query.GetPage<ExceptionLogResponse, ExceptionLog>(request, _mapper);
                var logs = await pagedQuery.ToListAsync();
                var mappedLogs = _mapper.Map<List<ExceptionLogResponse>>(logs);

                return new PagedResponseBase<List<ExceptionLogResponse>>(mappedLogs, request.PageNumber, request.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                var result = new PagedResponseBase<List<ExceptionLogResponse>>(null, request.PageNumber, request.PageSize, 0);
                result.ResponseStatusCode = HttpStatusCode.UnprocessableContent;
                result.AddExceptionLog(ex);
                return result;
            }
        }

        public async Task<PagedResponseBase<List<ApplicationUserLogResponse>>> GetApplicationUserLogAsync(GetApplicationUserLogRequest request)
        {
            try
            {
                var query = _applicationUserLogRepository.GetQueryable();

                var totalCount = await query.CountAsync();
                var pagedQuery = query.GetPage<ApplicationUserLogResponse, ApplicationUserLog>(request, _mapper);
                var logs = await pagedQuery.ToListAsync();
                var mappedLogs = _mapper.Map<List<ApplicationUserLogResponse>>(logs);

                return new PagedResponseBase<List<ApplicationUserLogResponse>>(mappedLogs, request.PageNumber, request.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                var result = new PagedResponseBase<List<ApplicationUserLogResponse>>(null, request.PageNumber, request.PageSize, 0);
                result.ResponseStatusCode = HttpStatusCode.UnprocessableContent;
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
