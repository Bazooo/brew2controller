// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Mutation
    {
        public async Task<GaugeValue> AddGaugeValue(string gaugeId, double value)
        {
            var gaugeValue = new GaugeValue
            {
                GaugeId = gaugeId,
                Value = value,
            };

            var topic = $"{gaugeId}_{nameof(Subscription.LatestGaugeValue)}";

            await this._database.GetGaugeValuesCollection().InsertOneAsync(gaugeValue);
            await this._sender.SendAsync(topic, gaugeValue);

            return gaugeValue;
        }
    }

    public partial class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<GaugeValue>> LatestGaugeValue(string gaugeId)
        {
            var topic = $"{gaugeId}_{nameof(this.LatestGaugeValue)}";
            return this._receiver.SubscribeAsync<string, GaugeValue>(topic);
        }
    }
}
