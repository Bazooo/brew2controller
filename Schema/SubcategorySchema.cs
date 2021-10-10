// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public async Task<IEnumerable<Subcategory>> GetSubcategories(string? categoryId)
        {
            var filter = categoryId != null
                ? Builders<Subcategory>.Filter.Empty
                : Builders<Subcategory>.Filter.Eq("CategoryId", categoryId);

            var result = await this._database.GetSubcategoriesCollection().FindAsync(filter);

            return await result.ToListAsync();
        }

        public async Task<Subcategory> GetSubcategory(string subcategoryId)
            => await this._database.GetSubcategoriesCollection().FindItemAsync(subcategoryId);
    }

    public partial class Mutation
    {
        public async Task<Subcategory> AddSubcategory(AddSubcategory newSubcategory)
        {
            var subcategory = new Subcategory(newSubcategory);
            await this._database.GetSubcategoriesCollection().InsertOneAsync(subcategory);
            return subcategory;
        }

        public async Task<Subcategory> UpdateSubcategory(UpdateSubcategory updatedSubcategory)
        {
            var subcategory = new Subcategory(updatedSubcategory);
            await this._database.GetSubcategoriesCollection().UpdateItemAsync(subcategory);
            return subcategory;
        }

        public async Task<OperationResult> DeleteSubcategory(string subcategoryId)
            => await this._database.GetSubcategoriesCollection().DeleteItemAsync(subcategoryId);
    }
}
