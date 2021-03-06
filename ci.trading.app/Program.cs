﻿using ci.trading.models.app;
using ci.trading.service.api;
using ci.trading.service.controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using System;
using System.IO;

namespace ci.trading.app
{
    class Program
    {
        static AppSettings appSettings = new AppSettings();
        static void Main(string[] args)
        {
            // setup services
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection = ConfigureServices(serviceCollection);

            // setup DI container
            // this will scan the .app and .service assemblies for interface/implementation.
            var container = new Container();
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.Assembly("ci.trading.service");
                    _.Assembly("ci.trading.app");
                    _.WithDefaultConventions();
                });
                // Populate the container using the service collection
                config.Populate(serviceCollection);
            });

            // Entry point to the app for now
            var entryControl = container.GetInstance<IEntryControl>();
            entryControl.StartResearch().Wait();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup logging
            serviceCollection.AddLogging(options =>
            {
                options.ClearProviders();
                options.AddConsole(options => options.IncludeScopes = true);
                options.AddDebug();
                options.AddFilter("Microsoft", LogLevel.Warning);
                options.AddFilter("System", LogLevel.Warning);
            });
            serviceCollection.AddOptions();


            // setup AppSettings configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            return serviceCollection;
        }
    }
}
