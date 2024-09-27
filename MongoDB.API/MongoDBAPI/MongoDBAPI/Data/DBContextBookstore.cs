using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDBAPI.Models;

namespace MongoDBAPI.Data
{
    public class DBContextBookstore
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName = "books";

        public DBContextBookstore(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
        {
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<BookModel> BookModel => _database.GetCollection<BookModel>(_collectionName);
    }
}
