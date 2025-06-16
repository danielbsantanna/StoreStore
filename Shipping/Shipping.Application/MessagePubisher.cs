using RabbitMQ.Client;
using System.Text;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using MassTransit;
using MessageContracts;
using Order.Application.Shared;

public class MessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishOrderStatus(OrderUpdateEvent order)
    {
        var publishEvent = new OrderReceiveUpdateEvent()
        {
            OrderId = order.Id,
            Status = order.Status
        };
        await _publishEndpoint.Publish(publishEvent, context => {
            context.SetRoutingKey("order-update-status");
        });

        Console.WriteLine($"[ShippingService] Sent Order update to Order ID {order.Id}. Status: {order.Status}");
    }
}
