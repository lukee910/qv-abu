using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;

namespace QvAbu.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = Setup();
            var scope = startup.ApplicationContainer.BeginLifetimeScope();

            var dbContext = scope.Resolve<IQuestionsContext>();
            var importExportService = scope.Resolve<IImportExportService>();

            startup.Configure((QuestionsContext)dbContext);

            //var selection = "";
            //var options = new[] {"1", "2"};
            //while (!options.Contains(selection))
            //{
            //    Console.WriteLine("Please select an action:");
            //    Console.WriteLine(" 1 -> Export");
            //    Console.WriteLine(" 2 -> Import");
            //    selection = Console.ReadLine();
            //}

            Console.WriteLine("Wie soll der Fragebogen heissen?");
            var name = Console.ReadLine();
            var task = importExportService.Import(name, args);
            //switch (selection)
            //{
            //    case "1":
            //        task = importExportService.Export();
            //        break;
            //    case "2":
            //        task = importExportService.Import();
            //        break;
            //    default:
            //        Console.WriteLine("This shouldn't happen, no selection detected. Please contact the system admin.");
            //        return;
            //}

            task.Wait();

            Console.WriteLine("\n\n\nTask completed.\nPress any key to exit...");
            Console.ReadKey();
        }

        private static Startup Setup()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            return startup;
        }
    }
}