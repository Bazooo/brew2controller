// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Schema.Subscriptions;
using HotChocolate;
using HotChocolate.Subscriptions;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<GaugeValue> AddGaugeValue(
            string gaugeId,
            double value,
            [Service] ITopicEventSender sender,
            [Service] IMongoCollection<GaugeValue> collection)
        {
            var gaugeValue = new GaugeValue
            {
                // todo: add gauge id (does not work)
                GaugeId = gaugeId,
                Value = value,
            };
            
            var topic = $"{gaugeId}_{nameof(Subscription.LatestGaugeValue)}";
            
            await collection.InsertOneAsync(gaugeValue);
            await sender.SendAsync(topic, gaugeValue);

            return gaugeValue;
        }
    }
}