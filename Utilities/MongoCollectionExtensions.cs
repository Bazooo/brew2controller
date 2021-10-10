using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Utilities
{
    public static class MongoCollectionExtensions
    {
        public static Task<T> FindItemAsync<T>(this IMongoCollection<T> collection, string id)
            where T : MongoCollectionItem
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).FirstAsync();
        }

        public static Task<ReplaceOneResult> UpdateItemAsync<T>(this IMongoCollection<T> collection, T item)
            where T : MongoCollectionItem
        {
            var filter = Builders<T>.Filter.Eq("Id", item.Id);
            return collection.ReplaceOneAsync(filter, item);
        }

        public static async Task<OperationResult> DeleteItemAsync<T>(this IMongoCollection<T> collection, string id)
            where T : MongoCollectionItem
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.DeleteOneAsync(filter);

            return new OperationResult { Worked = result.IsAcknowledged, };
        }
    }
}
