using MassTransit.Transports;
using Payment.Domain;
using Payment.Infrastructure;
using System;

namespace Payment.Application
{
    public class PaymentService
    {
        private readonly IPaymentRepository _repository;
        private MessagePublisher _messagePublisher;

        public PaymentService(IPaymentRepository repository, MessagePublisher messagePublisher)
        {
            _repository = repository;
            _messagePublisher = messagePublisher;
        }

        public async Task Create(PaymentEntity payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment), "Payment cannot be null");
            }

            payment.Created = DateTime.Now;
            await _repository.AddAsync(payment);

            await Task.Delay(5000);// Simulate payment queue processing
            payment.PaymentStatus = PaymentStatus.Processing.ToString();
            await _repository.UpdateAsync(payment);

            await _messagePublisher.PublishPaymentStatus(payment);

            if (await PaymentAnalysis(payment))
                payment.PaymentStatus = PaymentStatus.Completed.ToString();
            else
                payment.PaymentStatus = PaymentStatus.Failed.ToString();

            payment.Completed = DateTime.Now;
            await _repository.UpdateAsync(payment);
            await _messagePublisher.PublishPaymentStatus(payment);
        }

        public async Task<bool> PaymentAnalysis(PaymentEntity payment)
        {
            // Simulate payment analysis logic. This could involve checking payment details, contacting payment gateways, etc.
            await Task.Delay(10000); // Simulate some processing time
            return new Random().Next(4) != 0; // Randomly decide if the payment is successful or not
        }
    }
}
