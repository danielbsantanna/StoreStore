using MassTransit;
using MessageContracts;
using Payment.Application.Shared;
using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application
{

    public class PaymentConsumer : IConsumer<PaymentCreateEvent>
    {
        private PaymentService _paymentService;

        public PaymentConsumer(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Consume(ConsumeContext<PaymentCreateEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received Order: {message.OrderId}");

            PaymentEntity payment = new PaymentEntity { };
            Utils.CopyProperties(message, payment);

            await _paymentService.Create(payment);
        }
    }
}
