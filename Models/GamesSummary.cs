using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreCrudMongo.Models;

public class GamesSummary
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Username { get; set; } = null!;
    [BsonElement("trackedGames")]
    [JsonPropertyName("trackedGames")]
    public List<Game> Games { get; set; } = null!;
}