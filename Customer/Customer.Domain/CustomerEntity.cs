using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Customer.Domain
{
    public class CustomerEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

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

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
