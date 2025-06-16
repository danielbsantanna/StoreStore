using Payment.Domain;

namespace Payment.Infrastructure
{
    public interface IPaymentRepository
    {
        Task<List<PaymentEntity>> GetAllAsync();
        Task<PaymentEntity> GetByIdAsync(string id);
        Task AddAsync(PaymentEntity payment);
        Task UpdateAsync(PaymentEntity payment);
        Task DeleteAsync(string id);
    }
}
