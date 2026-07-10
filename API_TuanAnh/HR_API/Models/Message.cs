namespace HR_API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderDeviceId { get; set; }
        public string Content { get; set; }        
        public DateTime SentTime { get; set; }
        public bool IsSentToAll { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Section { get; set; }


    }
}
