using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QvAbu.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();
            var config = builder.Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(basePath)
                .UseConfiguration(config)
                .ConfigureServices(x =>
                    {
                        x.Add(new ServiceDescriptor(typeof(IConfigurationRoot), config));
                    }
                )
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .UseUrls(config["APPSETTING_QvAbuUrl"] ?? "http://0.0.0.0:55555")
                .Build();

            host.Run();
        }
    }
}
