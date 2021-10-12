using System;
using CSharpMongoGraphqlSubscriptions.OpcUA;
using CSharpMongoGraphqlSubscriptions.Schema;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using StackExchange.Redis;

namespace CSharpMongoGraphqlSubscriptions
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddSingleton<BrewClient>()
                .AddSingleton<BrewLogger>()
                .AddInMemorySubscriptions()
                .AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect("localhost:6379"));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>();

            // setup the database

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

            services.AddSingleton(_ =>
            {
                var client = new MongoClient(mongoClientSettings);
                var database = client.GetDatabase("brewdb");
                return database;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app
                .UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
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
