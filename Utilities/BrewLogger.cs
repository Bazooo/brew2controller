using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.LogModels;
using CSharpMongoGraphqlSubscriptions.Schema;
using HotChocolate.Subscriptions;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Utilities
{
    public class BrewLogger
    {
        private readonly IMongoDatabase _database;
        private readonly ITopicEventSender _sender;

        public BrewLogger(IMongoDatabase database, ITopicEventSender sender)
        {
            this._database = database;
            this._sender = sender;
        }

        public async Task<Log> AddLog(string message, LogType type)
        {
            var log = new Log
            {
                Message = message,
                Type = type,
            };

            await this._database.GetLogsCollection().InsertOneAsync(log);
            await this._sender.SendAsync($"{nameof(Subscription.GetLatestLog)}", log);

            return log;
        }

        public async Task<Log> AddUpdateLog(string message) =>
            await this.AddLog(message, LogType.Update);

        public async Task<Log> AddInfoLog(string message) =>
            await this.AddLog(message, LogType.Info);

        public async Task<Log> AddErrorLog(string message) =>
            await this.AddLog(message, LogType.Error);
    }
}
