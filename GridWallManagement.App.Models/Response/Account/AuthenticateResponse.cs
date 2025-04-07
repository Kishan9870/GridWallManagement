using GridWallManagement.App.Models.Common;
using GridWallManagement.App.Models.Response.Role;

namespace GridWallManagement.App.Models.Response.Account
{
    public class AuthenticateResponse : BaseDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string PasswordHash { get; set; }

        public string RoleId { get; set; }

        public string JwtToken { get; set; } = String.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public RolesResponse Roles { get; set; }
    }
}
