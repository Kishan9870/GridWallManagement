using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.UserLicense;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface IUserLicenseService
    {
        Task<PagedResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request);
    }
}
