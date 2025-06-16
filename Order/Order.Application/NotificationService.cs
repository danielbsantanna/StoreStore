using Microsoft.AspNetCore.SignalR;

namespace Order.Application
{

    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyClients(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
