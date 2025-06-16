using Customer.Domain;
using MongoDB.Driver;

namespace Customer.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IMongoCollection<CustomerEntity> _customer;

        public CustomerRepository(IMongoDatabase database)
        {
            _customer = database.GetCollection<CustomerEntity>("customer");
        }

        public async Task<List<CustomerEntity>> GetAllAsync()
        {
            return await _customer.Find(_ => true).ToListAsync();
        }

        public async Task<CustomerEntity> GetByIdAsync(string id)
        {
            return await _customer.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(CustomerEntity customer)
        {
            await _customer.InsertOneAsync(customer);
        }

        public async Task UpdateAsync(CustomerEntity customer)
        {
            await _customer.ReplaceOneAsync(c => c.Id == customer.Id, customer);
        }

        public async Task DeleteAsync(string id)
        {
            await _customer.DeleteOneAsync(c => c.Id == id);
        }
    }
}
