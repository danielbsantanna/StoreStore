using MassTransit;
using MessageContracts;
using Order.Application.Shared;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public class OrderConsumer : IConsumer<OrderReceiveUpdateEvent>
    {
        private OrderService _orderService;

        public OrderConsumer(OrderService orderService)
        { 
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<OrderReceiveUpdateEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received update for Order: {message.OrderId}");

            await _orderService.HandleOrderUpdate(message);
        }
    }
}
