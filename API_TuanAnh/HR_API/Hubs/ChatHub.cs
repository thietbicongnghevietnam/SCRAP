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

        //gui all thiet bi nhan duoc
        //public async Task RegisterDevice(string deviceId, string deviceName)
        //{
        //    var ip = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();

        //    var device = await _context.Devices
        //        .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

        //    if (device == null)
        //    {
        //        device = new Device
        //        {
        //            DeviceId = deviceId,
        //            DeviceName = deviceName,
        //            IpAddress = ip ?? "Unknown",
        //            LastActive = DateTime.UtcNow
        //        };
        //        _context.Devices.Add(device);
        //    }
        //    else
        //    {
        //        device.IpAddress = ip ?? device.IpAddress;
        //        device.DeviceName = deviceName;
        //        device.LastActive = DateTime.UtcNow;
        //    }

        //    await _context.SaveChangesAsync();
        //    await Groups.AddToGroupAsync(Context.ConnectionId, "AllDevices");

        //    await Clients.Caller.SendAsync("RegisterSuccess", "Đăng ký thành công");
        //}

        public async Task RegisterDevice(string deviceId, string deviceName)
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
                    LastActive = DateTime.UtcNow
                };

                _context.Devices.Add(device);
            }
            else
            {
                device.DeviceName = deviceName;
                device.IpAddress = ip ?? device.IpAddress;
                device.ConnectionId = Context.ConnectionId;
                device.LastActive = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            //truong hop gui tin nhan tu APP flutter len cho all cung nhan
            //neu chi can update co so du lieu => moi nguoi khong can nhan duoc => thi bo dong nay*****
            //de dong nay se duoc show all tren thiet bi
            await Groups.AddToGroupAsync(Context.ConnectionId, "AllDevices");

            await Clients.Caller.SendAsync("RegisterSuccess", "Đăng ký thành công");
        }
        //ham gui dung tin nhan cho 1 thiet bi
        public async Task SendMessageToDevice(string targetDeviceId, string content)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(x => x.DeviceId == targetDeviceId);

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

        public async Task SendMessageToAll(string senderDeviceId, string content)
        {
            //var message = new Message
            //{
            //    SenderDeviceId = senderDeviceId,
            //    Content = content,
            //    SentTime = DateTime.UtcNow,
            //    IsSentToAll = true
            //};
            var message = new Message
            {
                SenderDeviceId = senderDeviceId,
                Content = content,
                SentTime = DateTime.UtcNow,
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
    }
}