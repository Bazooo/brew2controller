using MongoDB.Bson.Serialization.Attributes;

namespace CSharpMongoGraphqlSubscriptions.Models.LogModels
{
    public class Log : MongoCollectionItem
    {
        public LogType Type { get; set; }

        public string Message { get; set; } = null!;

        [BsonIgnore]
        public string CreatedAt { get; set; } = null!;
    }

    public enum LogType
    {
        Error,
        Info,
        Update,
    }
}
