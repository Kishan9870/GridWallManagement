namespace GridWallManagement.App.Models.Response.UserLicense
{
    public class UserLicenseRenewalResponse
    {
        public Guid Id { get; set; }

        public Guid UserLicenseId { get; set; }

        public DateTime RenewalDate { get; set; } = DateTime.UtcNow;

        public DateTime PreviousExpirationTime { get; set; }

        public DateTime NewExpirationTime { get; set; }

        public int RenewalDuration { get; set; }

        public string? RenewedBy { get; set; }

        public string? Remarks { get; set; }
    }
}
