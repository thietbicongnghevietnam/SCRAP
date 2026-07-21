using HR_API.Data;
using HR_API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HR_API.Workers
{
    public class PushMessageWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PushMessageWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var hub = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub>>();

                    // Lấy tất cả tin nhắn chưa gửi
                    var messages = await context.Messages
                        .Where(x => !x.IsSentToAll)
                        .OrderBy(x => x.Id)
                        .ToListAsync(stoppingToken);

                    foreach (var message in messages)
                    {
                        var device = await context.Devices
                            .FirstOrDefaultAsync(x => x.DeviceId == message.SenderDeviceId);

                        if (device == null)
                            continue;

                        if (string.IsNullOrEmpty(device.ConnectionId))
                            continue;

                        await hub.Clients
                            .Client(device.ConnectionId)
                            .SendAsync(
                                "ReceiveMessage",
                                message.SenderDeviceId,
                                message.Content,
                                message.SentTime,
                                message.PhoneNumber,
                                message.Section,
                                message.Id,
                                message.Status ?? "Pending",
                                 message.UserName,    // ← Thêm
                                 message.UserID,    // ← Thêm
                                stoppingToken);

                        message.IsSentToAll = true;
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // đợi 1 giây
                await Task.Delay(1000, stoppingToken);
                // đợi 2 giây
                //await Task.Delay(2000, stoppingToken);

            }
        }
    }
}