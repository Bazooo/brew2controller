// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public async Task<IEnumerable<Category>> Categories()
        {
            var filter = Builders<Category>.Filter.Empty;
            var result = await this._database.GetCategoriesCollection().FindAsync(filter);

            return await result.ToListAsync();
        }
    }

    public partial class Mutation
    {
        public async Task<Category> AddCategory(AddCategory newCategory)
        {
            var category = new Category(newCategory);
            await this._database.GetCategoriesCollection().InsertOneAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategory(UpdateCategory updatedCategory)
        {
            var category = new Category(updatedCategory);
            await this._database.GetCategoriesCollection().UpdateItemAsync(category);
            return category;
        }

        public async Task<OperationResult> DeleteCategory(string categoryId)
        {
            var filter = Builders<Category>.Filter.Eq("Id", categoryId);
            await this._database.GetCategoriesCollection().DeleteOneAsync(filter);
            return new OperationResult
            {
                Worked = true,
            };
        }
    }

    public partial class Subscription
    {

    }
}
