// ReSharper disable UnusedMember.Global

using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Mutation
    {
        public async Task<TogglerValue> AddTogglerValue(string togglerId, TogglerStatus status)
        {
            var togglerValue = new TogglerValue
            {
                TogglerId = togglerId,
                Status = status,
            };

            var topic = $"{togglerId}_{nameof(Subscription.GetLatestTogglerValue)}";

            await this._database.GetTogglerValuesCollection().InsertOneAsync(togglerValue);
            await this._sender.SendAsync(topic, togglerValue);

            return togglerValue;
        }
    }

    public partial class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<TogglerValue>> GetLatestTogglerValue(string togglerId)
        {
            var topic = $"{togglerId}_{nameof(this.GetLatestTogglerValue)}";
            return this._receiver.SubscribeAsync<string, TogglerValue>(topic);
        }
    }
}
