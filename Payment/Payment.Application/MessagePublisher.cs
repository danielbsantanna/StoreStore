using Payment.Domain;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MassTransit;
using MessageContracts;
using Payment.Application.Shared;
using MassTransit.Transports;

namespace Payment.Application
{
    public class MessagePublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessagePublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishPaymentStatus(PaymentEntity payment)
        {
            var orderStatus = "";

            if (payment.PaymentStatus == "Processing")
                orderStatus = "PaymentProcessing";

            if (payment.PaymentStatus == "Completed")
                orderStatus = "PaymentAuthorized";

            if (payment.PaymentStatus == "Failed")
                orderStatus = "PaymentFailed";

            var publishEvent = new OrderReceiveUpdateEvent() { 
                OrderId = payment.OrderId,
                Status = orderStatus,
                PaymentId = payment.Id
            };
            await _publishEndpoint.Publish(publishEvent, context => {
                context.SetRoutingKey("order-update-status");
            });

            Console.WriteLine($"[PaymentService] Sent Payment Update for Order ID {payment.OrderId}");
        }
    }
}
