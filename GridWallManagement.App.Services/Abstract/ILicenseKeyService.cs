using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Request.LicenseKey;
using GridWallManagement.App.Models.Request.UserLicense;
using GridWallManagement.App.Models.Response.LicenseKey;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface ILicenseKeyService
    {
        Task<ResponseBase<GenerateLicenseKeyResponse>> GenerateLicenseKeyAsync(GenerateLicenseKeyRequest request);

        Task<ResponseBase<bool>> ValidateLicenseKey(ValidateLicenseKeyRequest request);

        Task<ResponseBase<bool>> RegisterUserLicense(RegisterUserLicenseRequest request);
    }
}
