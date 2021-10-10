using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Models.GaugeModels;
using CSharpMongoGraphqlSubscriptions.Models.TogglerModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels
{
    public partial class Subcategory: MongoCollectionItem
    {
        public string Name { get; set; } = null!;

        public int Rank { get; set; }

        // references

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;

        public async Task<Category> GetCategory([Service] IMongoDatabase database)
            => await database.GetCategoriesCollection().FindItemAsync(this.CategoryId);

        [BsonIgnore]
        public IEnumerable<Toggler> Togglers { get; set; } = Array.Empty<Toggler>();

        [BsonIgnore]
        public IEnumerable<Gauge> Gauges { get; set; } = Array.Empty<Gauge>();
    }
}
