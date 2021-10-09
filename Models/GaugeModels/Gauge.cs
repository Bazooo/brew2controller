using System;
using System.Collections.Generic;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.GaugeModels
{
    public class Gauge: MongoCollectionItem
    {
        public string PhysicalId { get; set; } = null!;


        public string Name { get; set; } = null!;


        public string Description { get; set; } = null!;


        public GaugeType Type { get; set; }
        
        
        public int Rank { get; set; }
        
        
        public bool Interactive { get; set; }
        
        // references


        [BsonRepresentation(BsonType.ObjectId)]
        public string SubcategoryId { get; set; } = null!;

        [BsonIgnore]
        public IEnumerable<GaugeValue> Values { get; set; } = Array.Empty<GaugeValue>();
    }

    public enum GaugeType
    {
        Temperature,
        Pressure,
    }
}