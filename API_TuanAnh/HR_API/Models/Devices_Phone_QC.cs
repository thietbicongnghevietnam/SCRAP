namespace HR_API.Models
{
    public class Devices_Phone_QC
    {
        public int ID { get; set; }
        public string? mathietbi { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bophan { get; set; }      // ← Section
        public string? UserName { get; set; }
        public string? UserID { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
