using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NetCoreCrudMongo.Models;

namespace NetCoreCrudMongo.Services;

public class MongoDBService
{
    private readonly IMongoCollection<GamesSummary> _gamesSummaryCollection;

    public MongoDBService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _gamesSummaryCollection = database.GetCollection<GamesSummary>(mongoDbSettings.Value.CollectionName);
    }

    public async Task CreateAsync(GamesSummary gamesSummary)
    {
        await _gamesSummaryCollection.InsertOneAsync(gamesSummary);
    }

    public async Task<List<GamesSummary>> GetAsync()
    {
        return await _gamesSummaryCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddGameToSummary(string id, Game game)
    {
        FilterDefinition<GamesSummary> filter = Builders<GamesSummary>.Filter.Eq("Id", id);
        UpdateDefinition<GamesSummary> update = Builders<GamesSummary>.Update.AddToSet<Game>("trackedGames", game);
        await _gamesSummaryCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteSummary(string id)
    {
        FilterDefinition<GamesSummary> filter = Builders<GamesSummary>.Filter.Eq("Id", id);
        await _gamesSummaryCollection.DeleteOneAsync(filter);
    }
}