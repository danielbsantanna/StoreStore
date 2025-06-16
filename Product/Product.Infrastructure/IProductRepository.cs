using Product.Domain;

namespace Product.Infrastructure
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAllAsync();
        Task<ProductEntity> GetByIdAsync(string id);
        Task AddAsync(ProductEntity customer);
        Task UpdateAsync(ProductEntity customer);
        Task DeleteAsync(string id);
    }
}
