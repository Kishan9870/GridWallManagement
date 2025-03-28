using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class ApplicationUserLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        [Required]
        [MaxLength(45)]
        public string PublicIPAddress { get; set; }

        [Required]
        [MaxLength(45)]
        public string LocalIPAddress { get; set; }

        public DateTime StartedTime { get; set; } = DateTime.UtcNow;

        public bool IsStarted { get; set; } = false;

        public bool IsStopped { get; set; } = false;

        public DateTime? StoppedTime { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
