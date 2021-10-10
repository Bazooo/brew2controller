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
    };

    public enum TogglerStatus
    {
        On,
        Off,
        Error,
    }
}
