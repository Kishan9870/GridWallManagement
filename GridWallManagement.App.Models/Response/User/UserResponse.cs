using GridWallManagement.App.Models.Common;
using GridWallManagement.App.Models.Response.Role;

namespace GridWallManagement.App.Models.Response.User
{
    public class UserResponse : BaseDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string PasswordHash { get; set; }

        public string RoleId { get; set; }

        public RolesResponse Roles { get; set; }
    }
}
