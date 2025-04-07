using GridWallManagement.App.Models.Request.Account;
using GridWallManagement.App.Models.Response.Account;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface IAccountService
    {
        Task<ResponseBase<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress, CancellationToken cancellationToken = default);
    }
}
