using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface IUtilityService
    {
        Task<ResponseBase<string>> GetSyncerDBConnection();
    }
}
