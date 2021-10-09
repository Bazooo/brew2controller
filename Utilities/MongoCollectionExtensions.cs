using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Utilities
{
    public static class MongoCollectionExtensions
    {
        public static Task<ReplaceOneResult> UpdateItemAsync<T>(this IMongoCollection<T> collection, T item)
            where T : MongoCollectionItem
        {
            var filter = Builders<T>.Filter.Eq("Id", item.Id);
            return collection.ReplaceOneAsync(filter, item);
        }
    }
}