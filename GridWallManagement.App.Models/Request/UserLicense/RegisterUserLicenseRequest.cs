namespace GridWallManagement.App.Models.Request.UserLicense
{
    public class RegisterUserLicenseRequest
    {
        public string Key { get; set; }
        public string PublicIpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string MACAddress { get; set; }
        public string RegisteredTime { get; set; }
        public string DeviceInfo { get; set; }
    }
}
