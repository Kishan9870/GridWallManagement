namespace GridWallManagement.App.Models.Request.Account
{
    public class AuthenticateRequest
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
