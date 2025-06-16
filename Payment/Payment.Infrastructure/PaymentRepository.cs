using MongoDB.Driver;
using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<PaymentEntity> _payment;

        public PaymentRepository(IMongoDatabase database)
        {
            _payment = database.GetCollection<PaymentEntity>("payment");
        }

        public async Task<List<PaymentEntity>> GetAllAsync()
        {
            return await _payment.Find(_ => true).ToListAsync();
        }

        public async Task<PaymentEntity> GetByIdAsync(string id)
        {
            return await _payment.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(PaymentEntity payment)
        {
            await _payment.InsertOneAsync(payment);
        }

        public async Task UpdateAsync(PaymentEntity payment)
        {
            await _payment.ReplaceOneAsync(c => c.Id == payment.Id, payment);
        }

        public async Task DeleteAsync(string id)
        {
            await _payment.DeleteOneAsync(c => c.Id == id);
        }
    }
}
