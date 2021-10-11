using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Opc.UaFx.Client;

namespace CSharpMongoGraphqlSubscriptions.OpcUA
{
    public class BrewListener : BackgroundService
    {
        private readonly IMongoDatabase _database;
        private readonly ITopicEventSender _sender;

        public BrewListener(IMongoDatabase database, ITopicEventSender sender)
        {
            this._database = database;
            this._sender = sender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var client = new OpcClient("opc.tcp://Win10-Bazoo:53530/OPCUA/SimulationServer");
            client.Connect();

            var subscription = client.SubscribeDataChange("ns=3;i=1001", this.HandleChange);

            while (!stoppingToken.IsCancellationRequested) { }
        }

        private void HandleChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            Console.WriteLine(e.Item.Value);
        }
    }
}
