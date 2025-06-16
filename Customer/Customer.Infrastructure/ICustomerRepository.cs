using Customer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure
{
    public interface ICustomerRepository
    {
        Task<List<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetByIdAsync(string id);
        Task AddAsync(CustomerEntity customer);
        Task UpdateAsync(CustomerEntity customer);
        Task DeleteAsync(string id);
    }
}
