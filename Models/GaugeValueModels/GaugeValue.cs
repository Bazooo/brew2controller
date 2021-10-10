using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels
{
    public class GaugeValue : MongoCollectionItem
    {
        public double Value { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string GaugeId { get; init; } = null!;

        public DateTime GetCreatedAt()
        {
            if (!ObjectId.TryParse(this.Id, out var objectId))
            {
                throw new Exception($"{this.Id} is not a valid objectId");
            }

            return objectId.CreationTime;
        }
    }
}
