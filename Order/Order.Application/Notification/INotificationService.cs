using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Notification
{
    public interface INotificationService
    {
        Task NotifyClients(string message);
    }
}
