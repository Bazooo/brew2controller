using System;
using System.Collections.Generic;
using CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.CategoryModels
{
    public partial class Category : MongoCollectionItem
    {
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;
        
        public int Rank { get; set; }

        [BsonIgnore]
        public IEnumerable<Subcategory> Subcategories { get; set; } = Array.Empty<Subcategory>();
    }
}