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
        public string? Status { get; set; }
        public string? UserName { get; set; }   // ← Thêm
        public string? UserID { get; set; }   // ← Thêm

        // ===== Thêm các cột này ===== tha tim
        public bool IsRead { get; set; } = false;
        public DateTime? ReadTime { get; set; }
        public string? ReadByUserID { get; set; }
        public string? ReadByUserName { get; set; }


    }
}
