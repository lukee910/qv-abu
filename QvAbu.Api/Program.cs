using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QvAbu.Api
{
    public class Program
    {
        const string Version = "1.1.4";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Application Version: " + Version);

            BuildWebHost(args).Run();
        }
        
        public static IWebHost BuildWebHost(string[] args)
        {
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddEnvironmentVariables();
            var config = builder.Build();

            return WebHost.CreateDefaultBuilder(args)
                // .UseKestrel()
                // .UseIISIntegration()
                .UseContentRoot(basePath)
                .UseConfiguration(config)
                .ConfigureServices(x =>
                    {
                        x.Add(new ServiceDescriptor(typeof(IConfigurationRoot), config));
                    }
                )
                .UseStartup<Startup>()
                .UseUrls(config["QvAbuUrl"] ?? "http://0.0.0.0:55555")
                .Build();
        }
    }
}
