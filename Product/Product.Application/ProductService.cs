using MessageContracts;
using Product.Domain;
using Product.Infrastructure;

namespace Product.Application
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly MessagePublisher _messagePublisher;

        public ProductService(IProductRepository repository, MessagePublisher messagePublisher)
        {
            _repository = repository;
            _messagePublisher = messagePublisher;
        }

        public async Task HandleOrderProducts(OrderUpdateEvent order)
        {
            // Here is where the products should be handled,  update the stock and set the order as processing. After this we wait until the lovely workers process the order and let ready for pickup
            // Sadly for now we will just simulate this process and ignore the stock stuff.

            order.Status = "Processing";
            await _messagePublisher.PublishOrderStatus(order);

            Task.Delay(15000).Wait(); 

            order.Status = "PickupReady";
            await _messagePublisher.PublishOrderStatus(order);
        }
    }
}
