using UserstoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UserstoreApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _UsersCollection;

    public UsersService(
        IOptions<UserstoreDatabaseSettings> UserstoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            UserstoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            UserstoreDatabaseSettings.Value.DatabaseName);
        _UsersCollection = mongoDatabase.GetCollection<User>(
            UserstoreDatabaseSettings.Value.UsersCollectionName);
    }
    public async Task<List<User>> GetAsync() =>
    await _UsersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _UsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newBook) =>
        await _UsersCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, User updatedBook) =>
        await _UsersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _UsersCollection.DeleteOneAsync(x => x.Id == id);

}