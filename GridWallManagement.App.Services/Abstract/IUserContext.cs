namespace GridWallManagement.App.Services.Abstract
{
    public interface IUserContext
    {
        public string UserId { get; set; }
        public string TimeZoneId { get; set; }
    }
}
