using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Payment.Domain
{
    public class PaymentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("orderId")]
        public string OrderId { get; set; }
        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }
        [BsonElement("paymentStatus")]
        public string PaymentStatus { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("completed")]
        public DateTime? Completed { get; set; }
    }
}
