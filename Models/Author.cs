﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryTask.Models;

public class Author
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }
}