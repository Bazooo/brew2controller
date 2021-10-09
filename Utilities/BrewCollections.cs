using MongoDB.Driver;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Models.GaugeModels;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Models.LogModels;
using CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels;
using CSharpMongoGraphqlSubscriptions.Models.TogglerModels;
using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;

namespace CSharpMongoGraphqlSubscriptions.Utilities
{
    public static class BrewCollections
    {
        public static IMongoCollection<Category> GetCategoriesCollection(this IMongoDatabase database)
            => database.GetCollection<Category>("categories");

        public static IMongoCollection<Gauge> GetGaugesCollection(this IMongoDatabase database)
            => database.GetCollection<Gauge>("gauges");

        public static IMongoCollection<GaugeValue> GetGaugeValuesCollection(this IMongoDatabase database)
            => database.GetCollection<GaugeValue>("gauge_values");

        public static IMongoCollection<Log> GetLogsCollection(this IMongoDatabase database)
            => database.GetCollection<Log>("logs");

        public static IMongoCollection<Subcategory> GetSubcategoriesCollection(this IMongoDatabase database)
            => database.GetCollection<Subcategory>("subcategories");

        public static IMongoCollection<Toggler> GetTogglersCollection(this IMongoDatabase database)
            => database.GetCollection<Toggler>("togglers");

        public static IMongoCollection<TogglerValue> GetTogglerValuesCollection(this IMongoDatabase database)
            => database.GetCollection<TogglerValue>("toggler_values");
    }
}
