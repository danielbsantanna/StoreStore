using MongoDB.Driver;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductEntity> _product;

        public ProductRepository(IMongoDatabase database)
        {
            _product = database.GetCollection<ProductEntity>("product");
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _product.Find(_ => true).ToListAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(string id)
        {
            return await _product.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(ProductEntity product)
        {
            await _product.InsertOneAsync(product);
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            await _product.ReplaceOneAsync(c => c.Id == product.Id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _product.DeleteOneAsync(c => c.Id == id);
        }
    }
}
