using MassTransit;
using MessageContracts;
using Order.Application.Shared;
using Shipping.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public class ShippingConsumer : IConsumer<OrderUpdateEvent>
    {
        private ShippingService _shippingService;

        public ShippingConsumer(ShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public async Task Consume(ConsumeContext<OrderUpdateEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received update for Order: {message.Id}");

            await _shippingService.HandleShipping(message);
        }
    }
}
