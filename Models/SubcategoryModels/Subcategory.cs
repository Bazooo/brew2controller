using System;
using System.Collections.Generic;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Models.GaugeModels;
using CSharpMongoGraphqlSubscriptions.Models.TogglerModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels
{
    public class Subcategory: MongoCollectionItem
    {
        public string Name { get; set; } = null!;
        
        public int Rank { get; set; }
        
        // references
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;
        
        [BsonIgnore]
        public Category Category { get; set; } = null!;

        [BsonIgnore]
        public IEnumerable<Toggler> Togglers { get; set; } = Array.Empty<Toggler>();
        
        [BsonIgnore]
        public IEnumerable<Gauge> Gauges { get; set; } = Array.Empty<Gauge>();
    }
}