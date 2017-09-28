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
        Task<(Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport);
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

        public async Task<(Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport)
        {
            var questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = name
            };
            this.questionnairesUow.QuestionnairesRepo.Add(questionnaire);
            await this.questionnairesUow.Complete();
            await this.questionsUow.Complete();

            var importedQuestions = new Dictionary<string, List<string>>();
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

                importedQuestions[fileName] = new List<string>();
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
                        case QuestionType.AssignmentQuestion:
                            parsingTask = this.ParseAssignmentQuestion(line, questionnaire.ID);
                            break;
                        default:
                            continue;
                    }

                    if (await parsingTask)
                    {
                        importedQuestions[fileName].Add(GetCsvString(line[0]));
                        questionsCount++;
                    }
                }
            }

            return (importedQuestions, erroredFiles);
        }

        private async Task<bool> ParseSimpleQuestion(List<string> line, Guid questionnaireId)
        {
            var question = new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = GetCsvString(line[0]),
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
                    Text = GetCsvString(line[i]),
                    IsCorrect = isCorrect
                });
            }

            this.questionsUow.SimpleQuestionsRepo.Add(question);
            await this.questionnairesUow.QuestionnairesRepo.AddQuestion(questionnaireId, question);
            await this.questionsUow.Complete();

            return true;
        }

        private async Task<bool> ParseTextQuestion(IReadOnlyList<string> line, Guid questionnaireId)
        {
            var question = new TextQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = GetCsvString(line[0]),
                Answer = new TextAnswer
                {
                    ID = Guid.NewGuid(),
                    Text = GetCsvString(line[1])
                }
            };

            this.questionsUow.TextQuestionsRepo.Add(question);
            await this.questionnairesUow.QuestionnairesRepo.AddQuestion(questionnaireId, question);
            await this.questionsUow.Complete();

            return true;
        }
        private async Task<bool> ParseAssignmentQuestion(IReadOnlyList<string> line, Guid questionnaireId)
        {
            var question = new AssignmentQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = GetCsvString(line[0]),
                Options = new List<AssignmentOption>(),
                Answers = new List<AssignmentAnswer>()
            };

            var optionTexts = line.Skip(1).TakeWhile(_ => !string.IsNullOrWhiteSpace(_));
            foreach (var optionText in optionTexts)
            {
                question.Options.Add(new AssignmentOption
                {
                    ID = Guid.NewGuid(),
                    Text = GetCsvString(optionText)
                });
            }

            var answerFields = line.SkipWhile(_ => !string.IsNullOrWhiteSpace(_))
                                   .SkipWhile(_ => string.IsNullOrWhiteSpace(_))
                                   .TakeWhile(_ => !string.IsNullOrWhiteSpace(_))
                                   .ToList();
            for(var i = 0; i + 1 < answerFields.Count(); i += 2)
            {
                var optionIndex = Convert.ToInt32(answerFields[i + 1]) - 1;
                
                question.Answers.Add(new AssignmentAnswer
                {
                    ID = Guid.NewGuid(),
                    Text = GetCsvString(answerFields[i]),
                    CorrectOption = question.Options[optionIndex]
                });
            }

            this.questionsUow.AssignmentQuestionsRepo.Add(question);
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

        private static string GetCsvString(string input)
        {
            return input.Trim('\"').Replace("\"\"", "\"");
        }

        #endregion
    }
}
