using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace QvAbu.CLI
{
    class Program
    {
        const int ShownQuestionsCountPerFile = 4;

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding(1252);

            Console.WriteLine(string.Join(";", args));
            Console.WriteLine("============");

            if (args.Length == 0)
            {
                Console.WriteLine("Please pass the names of the .csv-files to import as arguments.");
                Console.WriteLine();
                return;
            }

            var firstArg = args[0].ToLower();
            if (firstArg == "--help" || firstArg == "/?" || firstArg == "-h")
            {
                Console.WriteLine("Usage: Pass files with questions to import as arguments (either by console or by drag-and-drop onto the program).\n" +
                                  "The file format must be ;-separated .CSV.\n" +
                                  "Every run is grouped together in one Questionnaire.\n" +
                                  "To create import multiple questionnaires, multiple import runs are neccessary.\n" +
                                  "Each quesiton type (Multiple Choice, Assignment, Text) has to be in a separate file with the according file format.");
                Console.WriteLine("  Example: `run.cmd \"C:\\path\\to\\multiple choice questions.csv\" \"\\\\server\\path\\to\\text questions.csv\"`");
                Console.WriteLine("  Note: There are two ways to save a ;-separated .CSV in Excel:");
                Console.WriteLine("    1) Save it as \"Text (Tabstopp-getrennt) (.txt)\" and replace the tab character with ;");
                Console.WriteLine("    2) Change the Windows list separator to ; (see https://superuser.com/a/783065, but with ; instead of |)");
                Console.WriteLine();
                Console.WriteLine("File Formats:");
                Console.WriteLine("  Format info:");
                Console.WriteLine("    Whenever \" is used in a format description, it's for an informative purpose and shouldn't be written in the files.");
                Console.WriteLine("    \"...is correct\" has to be replaced by either \"true\" or \"false\".");
                Console.WriteLine("  Each file has the following base structure:");
                Console.WriteLine("    1st  Line: QuestionType");
                Console.WriteLine("    2nd  Line: Column Headers (Only informative, irrelevant for import)");
                Console.WriteLine("    3rd+ Line: Questions, 1 per line, in the specified format by QuestionType");
                Console.WriteLine();
                Console.WriteLine("Simple Question:");
                Console.WriteLine("  Simple questions are multiple choice questions in three types (called \"Simple Question Type\"):");
                Console.WriteLine("    0: One possible answer");
                Console.WriteLine("    1: Multiple possible answers, number of correct answers given");
                Console.WriteLine("    2: Multiple possible answers, number of correct answers not given (format for true/false questions)");
                Console.WriteLine("  Question Format:");
                Console.WriteLine("    \"Text;Simple Question Type;Answer 1 Text;Answer 1 is correct;Answer 2 Text; Answer 2 is  correct;...\"");
                Console.WriteLine("    \"Simple Question Type\" is the number from above");
                Console.WriteLine("  Example:");
                Console.WriteLine("    1;;;;;;;");
                Console.WriteLine("    ;;;;;;;");
                Console.WriteLine("    SingleChoiceQuestionText?;0;Answer1;false;Answer2;true;Answer3;false");
                Console.WriteLine();
                Console.WriteLine("Text Question:");
                Console.WriteLine("  Text questions are a questions that are answered in a free text format. The answer is given as a self-correction hint to the student.");
                Console.WriteLine("  Question Format:");
                Console.WriteLine("    \"Text;Suggested Answer Text\"");
                Console.WriteLine("  Example:");
                Console.WriteLine("    2;");
                Console.WriteLine("    ;");
                Console.WriteLine("    TextQuestionText?;ExampleAnswerText");
                Console.WriteLine();
                Console.WriteLine("Assignment Question:");
                Console.WriteLine("  Assignment questions are questions where answers have to be matched with the correct option.");
                Console.WriteLine("  Question Format:");
                Console.WriteLine("    \"Text;Option 1;Option 2;...;;Answer 1 Text; Answer 1 option number;Answer 2 Text; Answer 2 option number;...\"");
                Console.WriteLine("    \"Option number\" is the number of the option in the same line, e.g. \"1\" for \"Option 1\"");
                Console.WriteLine("    The options and answers are separated by an empty cell (here before \"Answer 1 Text\").");
                Console.WriteLine("  Example:");
                Console.WriteLine("    0;;;;;;;;;");
                Console.WriteLine("    ;;;;;;;;;");
                Console.WriteLine("    AssignmentQuestionText;Option1;Option2;;AnswerWithOption1;1;AnswerWithOption2;2;Answer2WithOption1;1");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return;
            }

            var startup = Setup();
            var scope = startup.ApplicationContainer.BeginLifetimeScope();

            var importExportService = scope.Resolve<IImportExportService>();

            startup.Configure();

            Console.WriteLine("How should the questionnaire be called?");
            var name = Console.ReadLine();
            Console.WriteLine("What tags should it have? (Enter any number of tags, separated by ',')");
            var tags = Console.ReadLine();
            Console.WriteLine("Importing...\n");
            try
            {
                var task = importExportService.Import(name, string.Join(",", tags.Split(",").Select(_ => _.Trim())), args);

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
                Console.WriteLine("An error occurred. Please confirm that the imported file is valid.\n");
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