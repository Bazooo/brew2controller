// ReSharper disable UnusedMember.Global

using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public TogglerValue TogglerValue()
        {
            return new TogglerValue
            {
                Status = TogglerStatus.Error,
            };
        }
    }

    public partial class Mutation
    {

    }

    public partial class Subscription
    {

    }
}
