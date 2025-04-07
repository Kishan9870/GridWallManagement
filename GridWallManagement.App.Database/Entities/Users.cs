using GridWallManagement.App.Database.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridWallManagement.App.Database.Entities
{
    public class Users : BaseModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string PasswordHash { get; set; }

        public string RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Roles Roles { get; set; }
    }
}