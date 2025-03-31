using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class UserLicenseRenewal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserLicenseId { get; set; }

        public DateTime RenewalDate { get; set; } = DateTime.UtcNow;

        public DateTime PreviousExpirationTime { get; set; }

        public DateTime NewExpirationTime { get; set; }

        public int RenewalDuration { get; set; } 

        [MaxLength(255)]
        public string? RenewedBy { get; set; } 

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        [ForeignKey("UserLicenseId")]
        public virtual UserLicense UserLicense { get; set; }
    }
}
