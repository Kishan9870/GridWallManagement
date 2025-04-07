using GridWallManagement.App.Database.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridWallManagement.App.Database.Entities
{
    public class RefreshToken : BaseModel
    {
        [Column("UserId")]
        public string UserId { get; set; }

        [StringLength(1000)]
        [Unicode(false)]
        public string Token { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Expires { get; set; }

        public bool IsExpired { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string? CreatedByIp { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Revoked { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? RevokedByIp { get; set; }

        [StringLength(1000)]
        [Unicode(false)]
        public string? ReplacedByToken { get; set; }

        [StringLength(3000)]
        [Unicode(false)]
        public string? DeleteReason { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.RefreshTokens))]
        public virtual Users User { get; set; }
    }
}
