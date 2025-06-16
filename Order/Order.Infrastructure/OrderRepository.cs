using MongoDB.Driver;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IMongoCollection<OrderEntity> _order;

        public OrderRepository(IMongoDatabase database)
        {
            _order = database.GetCollection<OrderEntity>("order");
        }

        public async Task<List<OrderEntity>> GetAllAsync()
        {
            return await _order.Find(_ => true).ToListAsync();
        }

        public async Task<OrderEntity> GetByIdAsync(string id)
        {
            return await _order.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(OrderEntity order)
        {
            await _order.InsertOneAsync(order);
        }

        public async Task UpdateAsync(OrderEntity order)
        {
            await _order.ReplaceOneAsync(c => c.Id == order.Id, order);
        }

        public async Task DeleteAsync(string id)
        {
            await _order.DeleteOneAsync(c => c.Id == id);
        }
    }
}
