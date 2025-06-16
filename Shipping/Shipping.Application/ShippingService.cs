using MessageContracts;
using Shipping.Domain;
using Shipping.Infrastructure;

namespace Shipping.Application
{
    public class ShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly MessagePublisher _messagePublisher;

        public ShippingService(IShippingRepository shippingRepository, MessagePublisher messagePublisher)
        {
            _shippingRepository = shippingRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task HandleShipping(OrderUpdateEvent order)
        {
            order.Status = "Shipped";
            ShippingOrder shippingOrder = new ShippingOrder
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                Crated = DateTime.UtcNow,
                Status = order.Status,
            };

            await _shippingRepository.AddAsync(shippingOrder);
            order.ShippingId = shippingOrder.Id;
            await _messagePublisher.PublishOrderStatus(order);

            Task.Delay(15000).Wait();

            order.Status = "Delivered";
            shippingOrder.Status = order.Status;
            await _shippingRepository.UpdateAsync(shippingOrder);
            await _messagePublisher.PublishOrderStatus(order);
        }
    }
}
