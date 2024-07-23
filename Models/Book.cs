using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryTask.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Category")]
    public string Category { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }
}