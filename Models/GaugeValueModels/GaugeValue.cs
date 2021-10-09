using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels
{
    public class GaugeValue: MongoCollectionItem
    {
        public double Value { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string GaugeId { get; set; } = null!;
    }
}