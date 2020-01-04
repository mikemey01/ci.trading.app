using ci.trading.service.app;
using Microsoft.Extensions.Configuration;
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

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            Console.WriteLine(appSettings.ApiPaper);
        }
    }
}
