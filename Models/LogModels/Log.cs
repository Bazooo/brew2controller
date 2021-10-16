using System;
using System.Globalization;
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

        public string GetDate()
            => this.GetCreatedAt().ToString("yyyy/MM/dd", CultureInfo.CreateSpecificCulture("en-GB"));

        public string GetTime()
            => this.GetCreatedAt().ToString("T", CultureInfo.CreateSpecificCulture("en-GB"));
    }

    public enum LogType
    {
        Error,
        Info,
        Update,
    }
}
