using System;
using Catalog.Entities;
using Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemRepository
    {
      

        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;


        public MongoDbItemsRepository(IOptions<CatalogDatabaseSettings> itemDatabaseSettings)
        {
            var mongoClient = new MongoClient(itemDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(itemDatabaseSettings.Value.DatabaseName);
            itemsCollection = mongoDatabase.GetCollection<Item>(itemDatabaseSettings.Value.ItemCollectionName);
        }


        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }


        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
             await itemsCollection.DeleteOneAsync(filter);
        }


        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();

        }


        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
           await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}

