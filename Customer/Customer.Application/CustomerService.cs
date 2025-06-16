using Customer.Domain;
using Customer.Infrastructure;

namespace Customer.Application
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
