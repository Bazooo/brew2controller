// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<Category> AddCategory(
            AddCategory newCategory,
            [Service] IMongoCollection<Category> collection)
        {
            var category = new Category(newCategory);
            await collection.InsertOneAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategory(
            UpdateCategory updatedCategory,
            [Service] IMongoCollection<Category> collection)
        {
            var category = new Category(updatedCategory);
            await collection.UpdateItemAsync(category);
            return category;
        }

        public async Task<OperationResult> DeleteCategory(
            string categoryId,
            [Service] IMongoCollection<Category> collection)
        {
            var filter = Builders<Category>.Filter.Eq("Id", categoryId);
            await collection.DeleteOneAsync(filter);
            return new OperationResult
            {
                Worked = true,
            };
        }
    }
}