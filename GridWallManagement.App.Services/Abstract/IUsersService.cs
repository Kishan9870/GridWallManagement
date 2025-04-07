using GridWallManagement.App.Models.Request.User;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Services.Common;

namespace GridWallManagement.App.Services.Abstract
{
    public interface IUsersService
    {
        Task<ResponseBase<List<UserResponse>>> GetAsync(GetUsersRequest request);
    }
}
