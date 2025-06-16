using Order.Domain;

namespace Order.Infrastructure
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetAllAsync();
        Task<OrderEntity> GetByIdAsync(string id);
        Task AddAsync(OrderEntity order);
        Task UpdateAsync(OrderEntity order);
        Task DeleteAsync(string id);
    }
}
