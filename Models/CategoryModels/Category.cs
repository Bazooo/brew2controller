// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Models.CategoryModels
{
    public partial class Category : MongoCollectionItem
    {
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int Rank { get; set; }

        public async Task<IEnumerable<Subcategory>> GetSubcategories([Parent] Category category, [Service] IMongoDatabase database)
        {
            var filter = Builders<Subcategory>.Filter.Eq("CategoryId", category.Id);
            var values = await database.GetSubcategoriesCollection().FindAsync(filter);
            var subcategories = await values.ToListAsync();

            return subcategories ?? new List<Subcategory>();
        }
    }
}
