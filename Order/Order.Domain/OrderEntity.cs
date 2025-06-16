using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;

namespace Order.Domain
{
    public class OrderEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
        [BsonElement("paymentId")]
        public string? PaymentId { get; set; }
        [BsonElement("shippingId")]
        public string? ShippingId { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("created")]
        public DateTime Created{ get; set; }
        [BsonElement("totalValue")]
        public Decimal TotalValue { get; set; }
        [BsonElement("items")]
        public List<OrderItem> Items { get; set; }
        public Payment Payment { get; set; }
    }
}
