// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace CSharpMongoGraphqlSubscriptions.Schema.Subscriptions
{
    public partial class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<GaugeValue>> LatestGaugeValue(
            string gaugeId,
            [Service] ITopicEventReceiver receiver)
        {
            var topic = $"{gaugeId}_{nameof(LatestGaugeValue)}";

            return receiver.SubscribeAsync<string, GaugeValue>(topic);
        }
    }
}