using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface ILicenseKeyService
    {
        Task<ResponseBase<List<UserLicenseResponse>>> GetUserLicensesAsync(GetUserLicensesRequest request);

        Task<ResponseBase<bool>> ValidateLicenseKey(ValidateLicenseKeyRequest request);

        Task<ResponseBase<bool>> RegisterUserLicense(RegisterUserLicenseRequest request);
    }
}
