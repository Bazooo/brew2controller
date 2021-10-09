// ReSharper disable UnusedMember.Global

using HotChocolate.Subscriptions;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        private readonly IMongoDatabase _database;

        public Query(IMongoDatabase database) => this._database = database;
    }

    public partial class Mutation
    {
        private readonly IMongoDatabase _database;
        private readonly ITopicEventSender _sender;

        public Mutation(IMongoDatabase database, ITopicEventSender sender)
        {
            this._database = database;
            this._sender = sender;
        }
    }

    public partial class Subscription
    {
        private readonly IMongoDatabase _database;
        private readonly ITopicEventReceiver _receiver;

        public Subscription(IMongoDatabase database, ITopicEventReceiver receiver)
        {
            this._database = database;
            this._receiver = receiver;
        }
    }
}
