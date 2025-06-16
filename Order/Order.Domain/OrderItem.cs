using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public class OrderItem
    {
        [BsonElement("productId")]
        public string ProductId { get; set; }
        [BsonElement("value")]
        public decimal Value { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
