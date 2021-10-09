// ReSharper disable UnusedMember.Global

using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;

namespace CSharpMongoGraphqlSubscriptions.Schema.Queries
{
    public partial class Query
    {
        public GaugeValue GaugeValue()
        {
            return new GaugeValue
            {
                Value = 123,
            };
        }
    }
}