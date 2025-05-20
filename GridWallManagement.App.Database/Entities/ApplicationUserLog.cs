using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class ApplicationUserLog
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        [Required]
        [MaxLength(45)]
        public string PublicIPAddress { get; set; } = string.Empty;

        [Required]
        [MaxLength(45)]
        public string LocalIPAddress { get; set; } = string.Empty;

        public DateTime? StartedTime { get; set; }

        public bool IsStarted { get; set; } = false;

        public bool IsStopped { get; set; } = false;

        public DateTime? StoppedTime { get; set; }

        public string? ErrorMessage { get; set; }

        public string? MACAddresses { get; set; }

        public string? DeviceInfo { get; set; }
    }
}
