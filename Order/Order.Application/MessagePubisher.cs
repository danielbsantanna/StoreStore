using System.Text;
using Order.Domain;
using Newtonsoft.Json;
using MassTransit;
using MessageContracts;
using Order.Application.Shared;
using System.Net.WebSockets;
using Order.Application;

public class MessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly NotificationService _notificationService;

    public MessagePublisher(IPublishEndpoint publishEndpoint, NotificationService notificationService)
    {
        _publishEndpoint = publishEndpoint;
        _notificationService = notificationService;
    }

    public async Task PublishOrderForPayment(Payment payment)
    {
        var publishEvent = new PaymentCreateEvent() { };
        Utils.CopyProperties(payment, publishEvent);
        await _publishEndpoint.Publish(publishEvent, context => {
            context.SetRoutingKey("payment-new");
        });

        Console.WriteLine($"[OrderService] Sent Payment Request for Order ID {payment.OrderId}");
    }

    public async Task PublishOrderStatus(OrderEntity order)
    {
        var publishEvent = new OrderUpdateEvent() { };
        Utils.CopyProperties(order, publishEvent);
        await _publishEndpoint.Publish(publishEvent, context =>
        {
            context.Headers.Set("orderStatus", order.Status);
        });
        Console.WriteLine($"[OrderService] Sent Order update to Order ID {order.Id}. Status: {order.Status}");
        _notificationService.NotifyClients(JsonConvert.SerializeObject(publishEvent)).Wait();
    }

}
