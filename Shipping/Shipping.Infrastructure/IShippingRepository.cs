using Shipping.Domain;

namespace Shipping.Infrastructure
{
    public interface IShippingRepository
    {
        Task<List<ShippingOrder>> GetAllAsync();
        Task<ShippingOrder> GetByIdAsync(string id);
        Task AddAsync(ShippingOrder shipping);
        Task UpdateAsync(ShippingOrder shipping);
        Task DeleteAsync(string id);
    }
}
