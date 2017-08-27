using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;

namespace QvAbu.CLI
{
    class Program
    {
        const int ShownQuestionsCountPerFile = 10;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass the names of the .csv-files to import as arguments.");
                Console.WriteLine();
                return;
            }

            var firstArg = args[0].ToLower();
            if (firstArg == "--help" || firstArg == "/?" || firstArg == "-h")
            {
                Console.WriteLine("Usage: Pass files with questions to import as arguments (either by console or by drag-and-drop onto the program). " +
                                  "The file format must be ;-separated .CSV." +
                                  "Every run is grouped together in one Questionnaire. " +
                                  "To create import multiple questionnaires, multiple import runs are neccessary.");
                Console.WriteLine("Example: \"C:\\path\\to\\multiple choice questions.csv\" \"\\\\server\\path\\to\\text questions.csv\"");
            }

            var startup = Setup();
            var scope = startup.ApplicationContainer.BeginLifetimeScope();

            var dbContext = scope.Resolve<IQuestionsContext>();
            var importExportService = scope.Resolve<IImportExportService>();

            startup.Configure((QuestionsContext)dbContext);

            Console.WriteLine("How should the questionnaire be called?");
            var name = Console.ReadLine();
            Console.WriteLine("Importing...\n");
            try
            {
                var task = importExportService.Import(name, args);

                (Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles) = task.GetAwaiter().GetResult();

                Console.WriteLine("Import Complete");

                Console.WriteLine("  Imported files:");
                foreach (var file in importedQuestions)
                {
                    Console.WriteLine($"    {file.Key}");
                    foreach (var question in file.Value.Take(ShownQuestionsCountPerFile))
                    {
                        Console.WriteLine($"      {question}");
                    }
                    if (file.Value.Count > ShownQuestionsCountPerFile)
                    {
                        Console.WriteLine($"      ... and {file.Value.Count - ShownQuestionsCountPerFile} more ({file.Value.Count} total)");
                        continue;
                    }
                }

                Console.WriteLine("\n  Import failed for files:");
                if (erroredFiles.Count == 0)
                {
                    Console.WriteLine("    None");
                }
                foreach (var file in erroredFiles)
                {
                    Console.WriteLine($"    {file}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred. Please confirm that the imported file is valid and/or relay this error message to the admins/developers.\n");
                Console.WriteLine("== ERROR MESSAGE");
                Console.WriteLine(e.Message);
                Console.WriteLine("== ERROR MESSAGE END");
            }

            Console.WriteLine("\nTask completed.\nPress any key to exit...");
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