using System;
using HotChocolate.Subscriptions;
using MongoDB.Driver;
using Opc.UaFx.Client;

namespace CSharpMongoGraphqlSubscriptions.OpcUA
{
    public class BrewClient : OpcClient
    {
        private readonly IMongoDatabase _database;
        private readonly ITopicEventSender _sender;

        public BrewClient(IMongoDatabase database, ITopicEventSender sender)
            : base(Environment.GetEnvironmentVariable("BREW_OPCUA_SERVER_ADDRESS"))
        {
            this._database = database;
            this._sender = sender;
            this.Connect();
        }
    }
}
