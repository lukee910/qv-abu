using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QvAbu.CLI.Wrappers;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models.Questions;

namespace QvAbu.CLI
{
    public interface IImportExportService
    {
        Task<(int importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport);
        Task Export();
    }

    public class ImportExportService : IImportExportService
    {
        #region Members

        private readonly IQuestionnairesUnitOfWork questionnairesUow;
        private readonly IQuestionsUnitOfWork questionsUow;
        private readonly IFile file;

        #endregion

        #region Ctors

        public ImportExportService(IQuestionnairesUnitOfWork questionnairesUow, 
            IQuestionsUnitOfWork questionsUow,
            IFile file)
        {
            this.questionnairesUow = questionnairesUow;
            this.questionsUow = questionsUow;
            this.file = file;
        }

        #endregion

        #region Props

        #endregion

        #region Methods

        public async Task<(int importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport)
        {
            var questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = name
            };
            this.questionnairesUow.QuestionnairesRepo.Add(questionnaire);
            await this.questionnairesUow.Complete();

            var erroredFiles = new List<string>();
            var questionsCount = 0;

            foreach (var fileName in filesToImport)
            {
                List<List<string>> csv;
                QuestionType type;
                try
                {
                    var text = await this.file.ReadAllText(fileName);
                    csv = text.Split('\n').Select(_ => _.Split(';').ToList()).ToList();
                    type = (QuestionType)Convert.ToInt32(csv[0][0]);
                }
                catch
                {
                    erroredFiles.Add(fileName);
                    continue;
                }

                var filteredCsv = new List<List<string>>();
                csv.Skip(2)
                    .ToList()
                    .ForEach(x => filteredCsv.Add(x.Where(_ => _ != "\r").ToList()));

                foreach (var line in filteredCsv)
                {
                    if (line.Count <= 1)
                    {
                        continue;
                    }

                    Task<bool> parsingTask;
                    switch (type)
                    {
                        case QuestionType.SimpleQuestion:
                            parsingTask = this.ParseSimpleQuestion(line, questionnaire.ID);
                            break;
                        case QuestionType.TextQuestion:
                            parsingTask = this.ParseTextQuestion(line, questionnaire.ID);
                            break;
                        default:
                            continue;
                    }

                    if (await parsingTask)
                    {
                        questionsCount++;
                    }
                }
            }

            return (questionsCount, erroredFiles);
        }

        private async Task<bool> ParseSimpleQuestion(List<string> line, Guid questionnaireId)
        {
            var question = new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = line[0],
                SimpleQuestionType = (SimpleQuestionType)Convert.ToInt32(line[1]),
                Answers = new List<SimpleAnswer>()
            };

            for (var i = 2; i < line.Count - 1; i += 2)
            {
                if (!bool.TryParse(line[i + 1], out bool isCorrect))
                {
                    continue;
                }

                question.Answers.Add(new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    Text = line[i],
                    IsCorrect = isCorrect
                });
            }

            this.questionsUow.SimpleQuestionsRepo.Add(question);
            await this.questionnairesUow.QuestionnairesRepo.AddQuestion(questionnaireId, question);
            await this.questionsUow.Complete();

            return true;
        }

        private async Task<bool> ParseTextQuestion(List<string> line, Guid questionnaireId)
        {
            var question = new TextQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = line[0],
                Answer = new TextAnswer
                {
                    ID = Guid.NewGuid(),
                    Text = line[1]
                }
            };

            this.questionsUow.TextQuestionsRepo.Add(question);
            await this.questionnairesUow.QuestionnairesRepo.AddQuestion(questionnaireId, question);
            await this.questionsUow.Complete();

            return true;
        }

        public Task Export()
        {
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

        #endregion
    }
}
