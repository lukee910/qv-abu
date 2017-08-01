using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;

namespace QvAbu.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Setup();

            var selection = "";
            var options = new[] {"1", "2"};
            while (!options.Contains(selection))
            {
                Console.WriteLine("Please select an action:");
                Console.WriteLine(" 1) Import");
                Console.WriteLine(" 2) Export");
                selection = Console.ReadLine();
            }
        }

        private static IServiceProvider Setup()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var dbContext = serviceProvider.GetService<QuestionsContext>();
            startup.Configure(dbContext);

            return serviceProvider;
        }
    }
}