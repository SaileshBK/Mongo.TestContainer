using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Models.Database;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Driver;

namespace Mongo.TestContainer.Repository;

internal class MongoRepository<T>(IMongoDbService mongoDbService) : IMongoRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection = mongoDbService
        .GetDatabase(TestContainerKeys.TestContainerDatabase)
        .GetCollection<T>(nameof(UserProfile));

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }
}
