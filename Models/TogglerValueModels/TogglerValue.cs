using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels
{
    public class TogglerValue : MongoCollectionItem
    {
        public TogglerStatus Status { get; set; }

        // references

        [BsonRepresentation(BsonType.ObjectId)]
        public string TogglerId { get; set; } = null!;

        public DateTime GetCreatedAt()
        {
            if (!ObjectId.TryParse(this.Id, out var objectId))
            {
                throw new Exception($"{this.Id} is not a valid objectId");
            }

            return objectId.CreationTime;
        }
    };

    public enum TogglerStatus
    {
        On,
        Off,
        Error,
    }
}
