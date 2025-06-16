using MessageContracts;
using Newtonsoft.Json;
using Order.Domain;
using Order.Infrastructure;

namespace Order.Application
{
    public class OrderService
    {
        private IOrderRepository _orderRepository;
        private MessagePublisher _messagePublisher;
        public OrderService(IOrderRepository orderRepository, MessagePublisher messagePublisher)
        {
            _orderRepository = orderRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<List<OrderEntity>> GetOrdersAsync()
        {
            try
            {
                return await _orderRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CreateOrderAsync(OrderEntity order)
        {
            try
            {
                if (order == null)
                {
                    throw new ArgumentNullException(nameof(order));
                }

                // Validate order properties (WIP)

                await _orderRepository.AddAsync(order);

                order.Payment.OrderId = order.Id ?? "";

                Task.Run(() => _messagePublisher.PublishOrderForPayment(order.Payment));

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task HandleOrderUpdate(OrderReceiveUpdateEvent orderUpdate)
        {
            var order = await _orderRepository.GetByIdAsync(orderUpdate.OrderId) ?? throw new ArgumentException($"Order with ID {orderUpdate.OrderId} not found.");

            order.Status = orderUpdate.Status;

            if (!String.IsNullOrWhiteSpace(orderUpdate.PaymentId))
            {
                order.PaymentId = orderUpdate.PaymentId;
            }

            if (!String.IsNullOrWhiteSpace(orderUpdate.ShippingId))
            {
                order.ShippingId = orderUpdate.ShippingId;
            }

            await _orderRepository.UpdateAsync(order);
            await _messagePublisher.PublishOrderStatus(order);
        }
    }
}
