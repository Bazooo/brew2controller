using System;
using CSharpMongoGraphqlSubscriptions.Models;
using CSharpMongoGraphqlSubscriptions.Models.CategoryModels;
using CSharpMongoGraphqlSubscriptions.Models.GaugeValueModels;
using CSharpMongoGraphqlSubscriptions.Models.TogglerValueModels;
using CSharpMongoGraphqlSubscriptions.Schema;
using CSharpMongoGraphqlSubscriptions.Schema.Mutations;
using CSharpMongoGraphqlSubscriptions.Schema.Subscriptions;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using StackExchange.Redis;
using Query = CSharpMongoGraphqlSubscriptions.Schema.Queries.Query;

namespace CSharpMongoGraphqlSubscriptions
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddInMemorySubscriptions()
                .AddRedisSubscriptions(sp => ConnectionMultiplexer.Connect("localhost:6379"));
            
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddMongoDbProjections();

            const string connectionString = "mongodb://root:pass@localhost";
            var mongoConnectionUrl = new MongoUrl(connectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoClientSettings.ClusterConfigurator = cb =>
            {
                // This will print the executed command to the console
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            var client = new MongoClient(mongoClientSettings);
            var database = client.GetDatabase("brewdb");

            services
                .AddSingleton(database.GetCollection<Category>("category"))
                .AddSingleton(database.GetCollection<GaugeValue>("gauge_values"))
                .AddSingleton(database.GetCollection<TogglerValue>("toggler_values"));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseWebSockets()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL().WithOptions(new GraphQLServerOptions
                    {
                        Tool =
                        {
                            DisableTelemetry = true,
                        },
                    });
                });
        }
    }
}