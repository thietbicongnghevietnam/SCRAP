using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HR_API.Hubs;
using HR_API.Data;
using Microsoft.EntityFrameworkCore;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly AppDbContext _context;

        public MessageController(IHubContext<ChatHub> hubContext, AppDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }
        //POST /api/Message/push-new-message
        //http://10.92.184.22:8036/swagger/index.html
        //http://10.92.184.22:8036/api/Message/push-new-message
        // API này sẽ được hệ thống khác gọi khi có tin nhắn mới
        [HttpPost("push-new-message")]
        public async Task<IActionResult> PushNewMessage([FromBody] PushMessageDto dto)
        {
            if (dto.MessageId <= 0)
                return BadRequest("MessageId không hợp lệ");

            // Lấy tin nhắn mới nhất từ database
            //var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == dto.MessageId);

            // Chỉ lấy những tin nhắn chưa gửi
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == dto.MessageId && !m.IsSentToAll);

            if (message == null)
                return NotFound("Không tìm thấy tin nhắn");

            // Đẩy realtime xuống tất cả Flutter apps
            //await _hubContext.Clients.Group("AllDevices")
            //    .SendAsync("ReceiveMessage",
            //        message.SenderDeviceId,
            //        message.Content,
            //        message.SentTime);

            //gui theo dung thiet bi ca nhan
            var device = await _context.Devices.FirstOrDefaultAsync(x => x.DeviceId == message.SenderDeviceId);

            if (device != null && !string.IsNullOrEmpty(device.ConnectionId))
            {
                //await _hubContext.Clients
                //    .Client(device.ConnectionId)
                //    .SendAsync(
                //        "ReceiveMessage",
                //        message.SenderDeviceId,
                //        message.Content,
                //        message.SentTime,
                //        message.PhoneNumber,
                //        message.Section,
                //        message.Id,           // ← Thêm Id vào đây (vị trí thứ 6)
                //        message.Status ?? "Pending"  // thứ 7 nếu có
                //    );
                await _hubContext.Clients
                .Client(device.ConnectionId)
                .SendAsync(
                    "ReceiveMessage",
                    message.SenderDeviceId,
                    message.Content,
                    message.SentTime,
                    message.PhoneNumber,
                    message.Section,
                    message.Id,                    // ← Thêm
                    message.Status ?? "Pending" ,   // ← Thêm
                    message.UserName,    // ← Thêm
                    message.UserID    // ← Thêm
                );
            }

            // Đánh dấu đã gửi
            message.IsSentToAll = true;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Đã đẩy tin nhắn mới đến tất cả thiết bị"
            });
        }

        // (Tùy chọn) Lấy danh sách thiết bị
        [HttpGet("devices")]
        public async Task<IActionResult> GetOnlineDevices()
        {
            var devices = await _context.Devices
                .OrderByDescending(d => d.LastActive)
                .ToListAsync();

            return Ok(devices);
        }

        // ====================== API MỚI - ĐÃ SỬA ======================
        //http://10.92.184.22:8036/api/Message/GetDeviceByUserId?userId=2012757&deviceId=b4fd64f2-a10c-44dc-8a7d-51d9aea82f0f

        [HttpGet("GetDeviceByUserId")]
        public async Task<IActionResult> GetDeviceByUserId(string userId, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "UserID không được để trống" });
            }

            // Tìm theo UserID trong bảng Devices_Phone_QC
            var device = await _context.Devices_Phone_QC
                .FirstOrDefaultAsync(d => d.UserID == userId);

            if (device == null)
            {
                return NotFound(new { message = "Không tìm thấy UserID này trong hệ thống" });
            }

            // Cập nhật thông tin
            device.mathietbi = deviceId;
            device.LastActive = DateTime.UtcNow;     // ← Luôn cập nhật thời gian mới nhất

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                section = device.Bophan,
                userID = device.UserID,
                userName = device.UserName,
                phoneNumber = device.PhoneNumber,
                mathietbi = device.mathietbi,
                lastActive = device.LastActive
            });
        }


    }

    // DTO dùng để trigger
    public class PushMessageDto
    {
        public int MessageId { get; set; }        // ID của tin nhắn vừa insert
    }
}