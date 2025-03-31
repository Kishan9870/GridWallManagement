namespace GridWallManagement.App.Models.Request
{
    public class RegisterUserLicenceRequest
    {
        public string Key { get; set; }
        public string PublicIpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string MACAddress { get; set; }
        public DateTime RegisteredTime { get; set; }
        public string DeviceInfo { get; set; }
    }
}
