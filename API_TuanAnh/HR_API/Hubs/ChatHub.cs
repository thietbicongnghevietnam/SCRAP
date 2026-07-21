using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;
using HR_API.Data;        // ← Dòng này quan trọng

namespace HR_API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        // ==================== ĐĂNG KÝ THIẾT BỊ ====================             
        public async Task RegisterDevice(string deviceId, string deviceName, string section, string userid)
        {
            var ip = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();

            var device = await _context.Devices
                .FirstOrDefaultAsync(x => x.DeviceId == deviceId);

            if (device == null)
            {
                device = new Device
                {
                    DeviceId = deviceId,
                    DeviceName = deviceName,
                    IpAddress = ip ?? "",
                    ConnectionId = Context.ConnectionId,
                    Section = section,           // ← Thêm
                    UserID = userid,           // ← Thêm
                    LastActive = DateTime.UtcNow
                };
                _context.Devices.Add(device);
            }
            else
            {
                device.DeviceName = deviceName;
                device.IpAddress = ip ?? device.IpAddress;
                device.ConnectionId = Context.ConnectionId;
                device.Section = section;        // ← Cập nhật
                device.UserID = userid;        // ← Cập nhật
                device.LastActive = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            // Join vào Group theo Section
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Section_{section}");

            await Clients.Caller.SendAsync("RegisterSuccess", "Đăng ký thành công");
        }

        // ==================== GỬI TIN NHẮN ====================
        public async Task SendMessageToSection(
    string senderDeviceId,
    string content,
    string? originalMessageId,
    string phoneNumber,
    string section,
    string username,
    string userid)
        {
            // Kiểm tra Section
            if (string.IsNullOrEmpty(section))
            {
                await Clients.Caller.SendAsync("Error", "Section không được để trống");
                return;
            }

            var message = new Message
            {
                SenderDeviceId = senderDeviceId,
                Content = content,
                SentTime = DateTime.UtcNow,
                IsSentToAll = false,           // Phân biệt với SendToAll
                PhoneNumber = phoneNumber,
                Section = section,
                Status = "Sent",
                UserName = username,
                UserID = userid,
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // === GỬI CHỈ ĐẾN NHÓM SECTION TƯƠNG ỨNG ===
            string groupName = $"Section_{section}";

            //await Clients.Group(groupName).SendAsync("ReceiveMessage",
            //    message.SenderDeviceId,
            //    content,
            //    message.SentTime,
            //    phoneNumber,
            //    section,
            //    message.Id,                    // ID tin nhắn (vị trí 6)
            //    message.Status ?? "Pending",   // Status (vị trí 7)
            //    message.UserName               // UserName (vị trí 8)
            //);

            // === GIẢI PHÁP: Gửi cho group NHƯNG LOẠI TRỪ máy gửi ===
            await Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("ReceiveMessage",
                message.SenderDeviceId,
                content,
                message.SentTime,
                phoneNumber,
                section,
                message.Id,
                message.Status ?? "Pending",
                message.UserName,
                message.UserID
            );

            // === Máy gửi vẫn nhận được tin nhắn (nhưng chỉ 1 lần, từ client local) ===
            // Hoặc bạn có thể gửi riêng cho Caller nếu muốn máy gửi cũng nhận qua server

            // Cập nhật tin nhắn gốc nếu là reply
            if (!string.IsNullOrEmpty(originalMessageId) && int.TryParse(originalMessageId, out int msgId))
            {
                var originalMsg = await _context.Messages.FindAsync(msgId);
                if (originalMsg != null)
                {
                    originalMsg.Status = "OK";
                    await _context.SaveChangesAsync();
                }
            }

            Console.WriteLine($"[SendMessageToSection] Gửi thành công đến Group {groupName} (trừ người gửi)");
        }

        //ham gui dung tin nhan cho 1 thiet bi
        public async Task SendMessageToDevice(string targetDeviceId, string content)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(x => x.DeviceId == targetDeviceId);

            if (device == null)
                return;

            await Clients.Client(device.ConnectionId)
                .SendAsync(
                    "ReceiveMessage",
                    targetDeviceId,
                    content,
                    DateTime.UtcNow
                );
        }

        public async Task SendMessageToAll_old(string senderDeviceId, string content)
        {            
            var message = new Message
            {
                SenderDeviceId = senderDeviceId,
                Content = content,
                SentTime = DateTime.UtcNow,
                IsSentToAll = true,
                PhoneNumber = "",
                Section = ""
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            //await Clients.Group("AllDevices").SendAsync("ReceiveMessage", senderDeviceId, content, message.SentTime);
            await Clients.Group("AllDevices")
                .SendAsync(
                    "ReceiveMessage",
                    senderDeviceId,
                    content,
                    message.SentTime,
                    message.PhoneNumber,
                    message.Section
                );
        }

        public async Task SendMessageToAll(
        string senderDeviceId,
        string content,
        string originalMessageId,
        string phoneNumber,
        string section,
        string username)
        {
            var message = new Message
            {
                SenderDeviceId = senderDeviceId,
                Content = content,
                SentTime = DateTime.UtcNow,
                IsSentToAll = true,
                PhoneNumber = phoneNumber,
                Section = section,
                Status = "Sent" ,  // hoặc "OK" tùy logic của bạn
                UserName = username // ← Thêm
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // === GỬI BROADCAST + TRẢ VỀ ID ===
            await Clients.All.SendAsync("ReceiveMessage",
                senderDeviceId,
                content,
                message.SentTime,
                phoneNumber,
                section,
                message.Id,           // ← Phải thêm Id (vị trí thứ 6)
                message.Status ?? "Pending",  // ← Thêm Status (vị trí thứ 7)
                message.UserName  // ← Thêm 
            );

            // === CẬP NHẬT TRẠNG THÁI TIN NHẮN GỐC ===
            if (!string.IsNullOrEmpty(originalMessageId) && int.TryParse(originalMessageId, out int msgId))
            {
                var originalMsg = await _context.Messages.FindAsync(msgId);
                if (originalMsg != null)
                {
                    originalMsg.Status = "OK";
                    // originalMsg.CompletedTime = DateTime.UtcNow; // nếu có cột này
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task MarkMessageAsRead(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

            if (message == null)
            {
                await Clients.Caller.SendAsync("Error", "Không tìm thấy tin nhắn");
                return;
            }

            // Lấy thiết bị đang đọc
            var device = await _context.Devices
                .FirstOrDefaultAsync(x => x.ConnectionId == Context.ConnectionId);

            if (device == null)
                return;

            message.IsRead = true;
            message.ReadTime = DateTime.UtcNow;
            message.ReadByUserID = device.UserID;

            // Nếu Devices có UserName thì dùng
            //message.ReadByUserName = device.UserName;

            await _context.SaveChangesAsync();

            // Phát cho tất cả client
            await Clients.All.SendAsync(
                "MessageRead",
                message.Id,
                message.ReadByUserID,
                message.ReadByUserName,
                message.ReadTime
            );
        }


    }
}