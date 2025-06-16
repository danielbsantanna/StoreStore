using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Product.Domain
{
    public class ProductEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("categoryName")]
        public string CategoryName { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; }
    }
}
