using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Shipping.Domain
{
    public class ShippingOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("orderId")]
        public string OrderId { get; set; }
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("carrierId")]
        public string CarrierId { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("region")]
        public string Region { get; set; }
        [BsonElement("postalCode")]
        public string PostalCode { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("created")]
        public DateTime Crated { get; set; }
        [BsonElement("freight")]
        public decimal Freight { get; set; }
    }
}
