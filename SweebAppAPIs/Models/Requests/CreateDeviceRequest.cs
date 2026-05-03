namespace SweebAppAPIs.Models.Requests
{
    public class CreateDeviceRequest
    {
        public int UserId { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceOS { get; set; } = string.Empty;
    }
}
