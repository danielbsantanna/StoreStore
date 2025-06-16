using MongoDB.Driver;
using Shipping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly IMongoCollection<ShippingOrder> _shipping;

        public ShippingRepository(IMongoDatabase database)
        {
            _shipping = database.GetCollection<ShippingOrder>("shipping");
        }

        public async Task<List<ShippingOrder>> GetAllAsync()
        {
            return await _shipping.Find(_ => true).ToListAsync();
        }

        public async Task<ShippingOrder> GetByIdAsync(string id)
        {
            return await _shipping.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(ShippingOrder shipping)
        {
            await _shipping.InsertOneAsync(shipping);
        }

        public async Task UpdateAsync(ShippingOrder shipping)
        {
            await _shipping.ReplaceOneAsync(c => c.Id == shipping.Id, shipping);
        }

        public async Task DeleteAsync(string id)
        {
            await _shipping.DeleteOneAsync(c => c.Id == id);
        }
    }
}
