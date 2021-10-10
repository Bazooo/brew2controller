// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.TogglerModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Schema
{
    public partial class Query
    {
        public async Task<IEnumerable<Toggler>> GetTogglers(string? subcategoryId)
        {
            var filter = subcategoryId != null
                ? Builders<Toggler>.Filter.Eq("SubcategoryId", subcategoryId)
                : Builders<Toggler>.Filter.Empty;

            var result = await this._database.GetTogglersCollection().FindAsync(filter);

            return await result.ToListAsync();
        }

        public async Task<Toggler> GetToggler(string togglerId) =>
            await this._database.GetTogglersCollection().FindItemAsync(togglerId);
    }

    public partial class Mutation
    {
        public async Task<Toggler> AddToggler(AddToggler newToggler)
        {
            var toggler = new Toggler(newToggler);
            await this._database.GetTogglersCollection().InsertOneAsync(toggler);
            await this._brewLogger.AddUpdateLog($"New toggler added: {toggler.Name}");

            return toggler;
        }

        public async Task<Toggler> UpdateToggler(UpdateToggler updatedToggler)
        {
            var toggler = new Toggler(updatedToggler);
            await this._database.GetTogglersCollection().UpdateItemAsync(toggler);
            await this._brewLogger.AddUpdateLog($"Toggler updated: {toggler.Name}");

            return toggler;
        }

        public async Task<OperationResult> DeleteToggler(string togglerId) =>
            await this._database.GetTogglersCollection().DeleteItemAsync(togglerId);
    }
}
