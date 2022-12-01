using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreCrudMongo.Models;

public class Game
{
    public string Name { get; set; } = null!;
    public bool Completed { get; set; } = false;
}