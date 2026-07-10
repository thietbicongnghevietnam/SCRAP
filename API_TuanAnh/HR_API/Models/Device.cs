namespace HR_API.Models
{
    //public class Device
    //{
    //    public int Id { get; set; }
    //    public string DeviceId { get; set; }      // Unique ID từ Flutter
    //    public string IpAddress { get; set; }
    //    public string DeviceName { get; set; }
    //    public DateTime LastActive { get; set; }
    //}

    public class Device
    {
        public int Id { get; set; }

        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string IpAddress { get; set; }

        public string? ConnectionId { get; set; }

        public DateTime LastActive { get; set; }
    }

}
