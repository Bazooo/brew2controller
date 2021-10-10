using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.LogModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.Execution;
using HotChocolate.Types;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public async Task<IEnumerable<Log>> GetLogs()
        {
            var filter = Builders<Log>.Filter.Empty;

            return await this._database
                .GetLogsCollection()
                .Find(filter)
                .SortByDescending(bson => bson.Id)
                .ToListAsync();
        }
    }

    public partial class Mutation
    {
        public async Task<Log> AddLog(string message, LogType type) =>
            await this._brewLogger.AddLog(message, type);
    }

    public partial class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<Log>> GetLatestLog() =>
            this._receiver.SubscribeAsync<string, Log>($"{nameof(this.GetLatestLog)}");
    }
}
