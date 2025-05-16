using GridWallManagement.App.Models.Request.Role;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface IRolesService
    {
        Task<PagedResponseBase<List<RolesResponse>>> GetAsync(GetRolesRequest request);
    }
}
