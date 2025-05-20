using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Entities
{
    public class ExceptionLog
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        [MaxLength(255)]
        public string ExceptionType { get; set; } = string.Empty;

        public string? Message { get; set; }

        public string? StackTrace { get; set; }

        public string? Source { get; set; }

        public string? InnerExceptionMessage { get; set; }

        [Required]
        [MaxLength(100)]
        public string Platform { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UserAgent { get; set; }

        [MaxLength(50)]
        public string? ClientIP { get; set; }

        public string? ErrorMsg1 { get; set; }
        public string? ErrorMsg2 { get; set; }
        public string? ErrorMsg3 { get; set; }
        public string? ErrorMsg4 { get; set; }
        public string? ErrorMsg5 { get; set; }
    }
}
