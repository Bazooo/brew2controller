// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.Execution;
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

            var topic = $"{gaugeId}_{nameof(Subscription.GetLatestGaugeValue)}";

            // todo: send shit to plc

            await this._database.GetGaugeValuesCollection().InsertOneAsync(gaugeValue);
            await this._sender.SendAsync(topic, gaugeValue);
            var gauge = await this._database.GetGaugesCollection().FindItemAsync(gaugeValue.GaugeId);
            await this._brewLogger.AddUpdateLog($"Updated gauge {gauge.Name} value: {gaugeValue.Value}");

            return gaugeValue;
        }
    }

    public partial class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<GaugeValue>> GetLatestGaugeValue(string gaugeId)
        {
            var topic = $"{gaugeId}_{nameof(this.GetLatestGaugeValue)}";
            return this._receiver.SubscribeAsync<string, GaugeValue>(topic);
        }
    }
}
