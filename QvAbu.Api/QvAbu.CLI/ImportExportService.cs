using System;
using System.IO;
using System.Threading.Tasks;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models.Questions;

namespace QvAbu.CLI
{
    interface IImportExportService
    {
        Task Import();
        Task Export();
    }

    class ImportExportService : IImportExportService
    {
        #region Members

        private readonly IQuestionnairesUnitOfWork questionnairesUow;
        private readonly IQuestionsUnitOfWork questionsUow;

        #endregion

        #region Ctors

        public ImportExportService(IQuestionnairesUnitOfWork questionnairesUow, 
            IQuestionsUnitOfWork questionsUow)
        {
            this.questionnairesUow = questionnairesUow;
            this.questionsUow = questionsUow;
        }

        #endregion

        #region Props

        #endregion

        #region Methods

        public Task Export()
        {
            this.WriteHeader("Export");

            //var currentDirectory = Directory.GetCurrentDirectory();
            //var targetDirectory = currentDirectory;
            //do
            //{
            //    Console.WriteLine("Please enter the path where the files should be exported.");
            //    Console.WriteLine($"Leave empty for current folder ({currentDirectory}).");
            //} while (!new DirectoryInfo(targetDirectory).Exists);

            //Console.WriteLine($"\n\nStarting export to \"{currentDirectory}\"...");

            // TODO: Export
            return Task.CompletedTask;
        }

        public Task Import()
        {
            WriteHeader("Import");

            // TODO: Import
            return Task.CompletedTask;
        }

        private void WriteHeader(string text)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("==================");
            Console.WriteLine("= " + text);
            Console.WriteLine("==================");
        }

        #endregion
    }
}
