using AutoMapper;
using GridWallManagement.App.Services.Abstract;
using GridWallManagement.App.Services.Common;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class UtilityService : ServiceBase, IUtilityService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UtilityService(IConfiguration config,
                              IMapper mapper,
                              IUserContext userContext) : base(mapper, userContext)
        {
            _config = config;
            _mapper = mapper;
        }

        public async Task<ResponseBase<string>> GetSyncerDBConnection()
        {
            try
            {
                var connection = _config.GetSection("DatabaseConnection:SyncerDBConnection").Value;

                if (string.IsNullOrEmpty(connection))
                    throw new Exception("Database connection not available.");

                return new ResponseBase<string>(connection);
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<string>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
