using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleInv;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
}