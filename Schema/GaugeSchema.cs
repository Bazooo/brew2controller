// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.GaugeModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public async Task<IEnumerable<Gauge>> GetGauges(string? subcategoryId)
        {
            var filter = subcategoryId != null
                ? Builders<Gauge>.Filter.Eq("SubcategoryId", subcategoryId)
                : Builders<Gauge>.Filter.Empty;

            var result = await this._database.GetGaugesCollection().FindAsync(filter);

            return await result.ToListAsync();
        }

        public async Task<Gauge> GetGauge(string gaugeId)
            => await this._database.GetGaugesCollection().FindItemAsync(gaugeId);
    }

    public partial class Mutation
    {
        public async Task<Gauge> AddGauge(AddGauge newGauge)
        {
            var gauge = new Gauge(newGauge);
            await this._database.GetGaugesCollection().InsertOneAsync(gauge);
            return gauge;
        }

        public async Task<Gauge> UpdateGauge(UpdateGauge updatedGauge)
        {
            var gauge = new Gauge(updatedGauge);
            await this._database.GetGaugesCollection().UpdateItemAsync(gauge);
            return gauge;
        }

        public async Task<OperationResult> DeleteGauge(string gaugeId)
            => await this._database.GetGaugesCollection().DeleteItemAsync(gaugeId);
    }
}
