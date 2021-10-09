using System;
using System.Collections.Generic;
using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.TogglerModels
{
    public class Toggler: MongoCollectionItem
    {
        public string PhysicalId { get; set; } = null!;
        
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        
        public bool Interactive { get; set; }
        
        public int Rank { get; set; }
        
        // references

        [BsonRepresentation(BsonType.ObjectId)]
        public string SubcategoryId { get; set; } = null!;

        [BsonIgnore]
        public IEnumerable<TogglerValue> Values { get; set; } = Array.Empty<TogglerValue>();
    }
}