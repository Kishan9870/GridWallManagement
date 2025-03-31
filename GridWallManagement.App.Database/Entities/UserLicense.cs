using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class UserLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(45)]
        public string? PublicIPAddress { get; set; }

        [MaxLength(45)]
        public string? LocalIPAddress { get; set; }

        [MaxLength(255)]
        public string? MacAddress { get; set; }

        [Required]
        [MaxLength(1000)]
        public string LicenseKey { get; set; } = string.Empty;

        public DateTime RegisteredTime { get; set; } = DateTime.UtcNow;

        public DateTime? ExpirationTime { get; set; }

        public int? LicenseDuration { get; set; } 

        [MaxLength(1000)]
        public string? DeviceInfo { get; set; }

        public virtual ICollection<UserLicenseRenewal> Renewals { get; set; } = new HashSet<UserLicenseRenewal>();
    }
}
