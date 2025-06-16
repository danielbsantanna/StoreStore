using MassTransit;
using MessageContracts;
using Order.Application.Shared;
using Product.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public class ProductConsumer : IConsumer<OrderUpdateEvent>
    {
        private ProductService _productService;

        public ProductConsumer(ProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<OrderUpdateEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received update for Order: {message.Id}");

            await _productService.HandleOrderProducts(message);
        }
    }
}
