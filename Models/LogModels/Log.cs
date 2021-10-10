using System;
using MongoDB.Bson;

namespace CSharpMongoGraphqlSubscriptions.Models.LogModels
{
    public class Log : MongoCollectionItem
    {
        public LogType Type { get; set; }

        public string Message { get; set; } = null!;

        public DateTime GetCreatedAt()
        {
            if (!ObjectId.TryParse(this.Id, out var objectId))
            {
                throw new Exception($"{this.Id} is not a valid objectId");
            }

            return objectId.CreationTime;
        }
    }

    public enum LogType
    {
        Error,
        Info,
        Update,
    }
}
