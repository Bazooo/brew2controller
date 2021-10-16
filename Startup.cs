using System;
using CSharpMongoGraphqlSubscriptions.OpcUA;
using CSharpMongoGraphqlSubscriptions.Schema;
using CSharpMongoGraphqlSubscriptions.Utilities;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using StackExchange.Redis;

namespace CSharpMongoGraphqlSubscriptions
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var mongodbConnectionString = Environment.GetEnvironmentVariable("BREW_DB_URI") ?? "mongodb://root:pass@localhost";
            var redisUri = Environment.GetEnvironmentVariable("REDIS_URI") ?? "localhost:6379";

            services
                .AddCors()
                .AddSingleton<BrewClient>()
                .AddSingleton<BrewLogger>()
                .AddInMemorySubscriptions()
                .AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect(redisUri));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>();

            // setup the database

            var mongoConnectionUrl = new MongoUrl(mongodbConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

            services.AddSingleton(_ =>
            {
                var client = new MongoClient(mongoClientSettings);
                var database = client.GetDatabase("brewdb");
                return database;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseWebSockets()
                .UseCors(builder =>
                {
                    if (env.IsDevelopment())
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                })
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL()
                        .WithOptions(new GraphQLServerOptions { Tool =
                        {
                            Enable = env.IsDevelopment(),
                            DisableTelemetry = true,
                        }, });
                });

            if (env.IsProduction())
            {
                app
                    .UseFileServer(new FileServerOptions
                    {
                        FileProvider = new PhysicalFileProvider(Environment.GetEnvironmentVariable("BREW_APP_DIRECTORY")),
                        RequestPath = "",
                    });
            }
        }
    }
}
