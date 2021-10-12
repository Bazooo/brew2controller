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
            : base("opc.tcp://Win10-Bazoo.mshome.net:53530/OPCUA/SimulationServer")
        {
            this._database = database;
            this._sender = sender;
            this.Connect();
            Console.WriteLine("test");
        }
    }
}
