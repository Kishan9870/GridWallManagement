using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class UserLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(45)]
        public string PublicIPAddress { get; set; }

        [MaxLength(45)]
        public string LocalIPAddress { get; set; }
        
        public string LicenseKey { get; set; } = string.Empty;
        
        public DateTime RegisteredTime { get; set; } = DateTime.UtcNow;
        
        public DateTime? ExpirationTime { get; set; }
        
        public string? DeviceInfo { get; set; }
    }
}
