using ci.trading.app.controllers;
using ci.trading.service.api;
using ci.trading.service.app;
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
            Console.WriteLine("Hello World!");

            // setup logging
            var services = new ServiceCollection()
                               .AddLogging();

            // setup DI container
            // this will scan the .app and .service assemblies for interface/implementation.
            var container = new Container();
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.Assembly("ci.trading.service");
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                });
                // Populate the container using the service collection
                config.Populate(services);
            });

            // get an instance of the service provider.
            var serviceProvider = container.GetInstance<IServiceProvider>();
            var logger = serviceProvider.GetService<ILoggerFactory>()
                                        .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            // Entry point to the app
            var mainController = container.GetInstance<IMainController>();
            mainController.StartTrading();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            Console.WriteLine(appSettings.ApiPaper);
        }
    }
}
