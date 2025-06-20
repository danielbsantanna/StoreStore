using MassTransit;
using Moq;
using Order.Application;
using Order.Application.Notification;
using Order.Domain;
using Order.Infrastructure;

namespace Order.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrder()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var mockEndpoint = new Mock<IPublishEndpoint>();
            var mockNotification = new Mock<INotificationService>();
            var mockPublisher = new MessagePublisher(mockEndpoint.Object, mockNotification.Object);
            var service = new OrderService(mockRepo.Object, mockPublisher);
            var order = new OrderEntity
            {
                CustomerId = "1",
                Created = DateTime.Now,
                Status = "Pending",
                TotalValue = 260,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = "1", Quantity = 1, Value = 120 },
                    new OrderItem { ProductId = "2", Quantity = 1, Value = 45 },
                    new OrderItem { ProductId = "3", Quantity = 1, Value = 95 }
                },
                Payment = new Payment
                {
                    PaymentMethod = "CreditCard",
                    PaymentStatus = "Pending",
                }
            };

            await service.CreateOrderAsync(order);

            mockRepo.Verify(r => r.AddAsync(It.Is<OrderEntity>(o =>
                o.CustomerId == "1" && o.TotalValue == 260 && o.Status == "Pending")), Times.Once);
        }
    }
}
