// ReSharper disable UnusedMember.Global

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

        public Mutation(IMongoDatabase database) => this._database = database;
    }

    public partial class Subscription
    {
        private readonly IMongoDatabase _database;

        public Subscription(IMongoDatabase database) => this._database = database;
    }
}
