using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface ILicenceKeyService
    {
        Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request);
    }
}
