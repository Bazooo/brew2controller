// ReSharper disable UnusedMember.Global

using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;

namespace CSharpMongoGraphqlSubscriptions.Schema.Queries
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
}