using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CSharpMongoGraphqlSubscriptions.Models.GaugeModels
{
    public partial class Gauge : MongoCollectionItem
    {
        public string PhysicalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public GaugeType Type { get; set; }

        public int Rank { get; set; }

        public bool Interactive { get; set; }

        // references

        [BsonRepresentation(BsonType.ObjectId)]
        public string SubcategoryId { get; set; } = null!;

        public async Task<Subcategory> GetSubcategory([Service] IMongoDatabase database) =>
            await database.GetSubcategoriesCollection().FindItemAsync(this.SubcategoryId);

        public async Task<IEnumerable<GaugeValue>> GetValues([Service] IMongoDatabase database)
        {
            var filter = Builders<GaugeValue>.Filter.Eq("GaugeId", this.Id);
            var values = await database.GetGaugeValuesCollection().FindAsync(filter);

            if (values != null)
            {
                return new List<GaugeValue>();
            }

            var gaugeValues = await values.ToListAsync();

            return gaugeValues ?? new List<GaugeValue>();
        }
    }

    public enum GaugeType
    {
        Temperature,
        Pressure,
    }
}
