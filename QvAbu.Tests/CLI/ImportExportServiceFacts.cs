using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using QvAbu.CLI;
using QvAbu.CLI.Wrappers;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models;
using QvAbu.Data.Models.Questions;
using Xunit;

namespace QvAbu.Api.Tests.CLI
{
    public class ImportExportServiceFacts
    {
        private Func<RevisionEntity, RevisionEntity, bool> matchEntity = (left, right) =>
        {
            left.Should().BeEquivalentTo(right);
            return true;
        };

        [Fact]
        public async Task ImportsSimpleQuestions()
        {
            // Arrange
            const string name = "name";
            var fileNames = new[] {"file1"};

            var questionnaire = new Questionnaire
            {
                Revision = 1,
                Name = name
            };
            var question1 = new SimpleQuestion
            {
                Revision = 1,
                SimpleQuestionType = SimpleQuestionType.SingleChoice,
                Text = "Text1",
                Answers = new List<SimpleAnswer>
                {
                    new SimpleAnswer
                    {
                        IsCorrect = true,
                        Text = "Answer1.1"
                    },
                    new SimpleAnswer
                    {
                        IsCorrect = false,
                        Text = "Answer1.2"
                    }
                }
            };
            var answers1 = question1.Answers.ToList();
            var question2 = new SimpleQuestion
            {
                Revision = 1,
                SimpleQuestionType = SimpleQuestionType.TrueFalse,
                Text = "Text2",
                Answers = new List<SimpleAnswer>
                {
                    new SimpleAnswer
                    {
                        IsCorrect = false,
                        Text = "Answer2.1"
                    },
                    new SimpleAnswer
                    {
                        IsCorrect = true,
                        Text = "Answer2.2"
                    },
                    new SimpleAnswer
                    {
                        IsCorrect = true,
                        Text = "Answer2.3"
                    }
                }
            };
            var answers2 = question2.Answers.ToList();
            var fileText = "1;;;;;;;\n" +
                "Text;Typ;Text Antwort 1;Anwort korrekt?;Text Antwort 2;;;\n" +
                $"\"{question1.Text}\";{(int)question1.SimpleQuestionType};{answers1[0].Text};{answers1[0].IsCorrect};{answers1[1].Text};{answers1[1].IsCorrect};;\r\n" +
                $"{question2.Text};{(int)question2.SimpleQuestionType};\"{answers2[0].Text}\";{answers2[0].IsCorrect};{answers2[1].Text};{answers2[1].IsCorrect};{answers2[2].Text};{answers2[2].IsCorrect}\n";

            var expectedImportedQuestions = new Dictionary<string, List<string>>
            {
                [fileNames[0]] = new List<string>
                {
                    question1.Text,
                    question2.Text
                }
            };

            var questionsUow = A.Fake<IQuestionsUnitOfWork>();
            var questionnairesUow = A.Fake<IQuestionnairesUnitOfWork>();
            var file = A.Fake<IFile>();

            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[0])))
                .Returns(fileText);

            var testee = new ImportExportService(questionnairesUow, questionsUow, file);

            // Act
            (Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles) = await testee.Import(name, fileNames);

            // Assert
            importedQuestions.Should().BeEquivalentTo(expectedImportedQuestions);
            erroredFiles.Should().BeNullOrEmpty();

            A.CallTo(() => questionnairesUow.QuestionnairesRepo.Add(A<Questionnaire>.That.Matches(_ => this.matchEntity(_, questionnaire))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.SimpleQuestionsRepo.Add(A<SimpleQuestion>.That.Matches(_ => this.matchEntity(_, question1))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.SimpleQuestionsRepo.Add(A<SimpleQuestion>.That.Matches(_ => this.matchEntity(_, question2))))
                .MustHaveHappened();

            A.CallTo(() => questionnairesUow.Complete())
                .MustHaveHappened();
            A.CallTo(() => questionsUow.Complete())
                .MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [Fact]
        public async Task ImportsTextQuestions()
        {
            // Arrange
            const string name = "text question";
            var fileNames = new[] { "file1", "file2" };

            var questionnaire = new Questionnaire
            {
                Revision = 1,
                Name = name
            };
            var question1 = new TextQuestion
            {
                Revision = 1,
                Text = "TextQuestion1",
                Answer = new TextAnswer
                {
                    Text = "TextAnswer1"
                }
            };
            var question2 = new TextQuestion
            {
                Revision = 1,
                Text = "TextQuestion2",
                Answer = new TextAnswer
                {
                    Text = "TextAnswer2"
                }
            };
            var question3 = new TextQuestion
            {
                Revision = 1,
                Text = "TextQuestion3",
                Answer = new TextAnswer
                {
                    Text = "TextAnswer3"
                }
            };
            var fileText1 = "2;\n" +
                           "Text;Antwort\n" +
                           $"\"{question1.Text}\";{question1.Answer.Text}\n" +
                           $"{question2.Text};{question2.Answer.Text}\n" +
                           $"{question3.Text};\"{question3.Answer.Text}\"";
            var question4 = new TextQuestion
            {
                Revision = 1,
                Text = "TextQuestion4",
                Answer = new TextAnswer
                {
                    Text = "TextAnswer4"
                }
            };
            var fileText2 = "2\n" +
                            "Text;Antwort\n" +
                            $"{question4.Text};{question4.Answer.Text}\n";

            var expectedImportedQuestions = new Dictionary<string, List<string>>
            {
                [fileNames[0]] = new List<string>
                {
                    question1.Text,
                    question2.Text,
                    question3.Text
                },
                [fileNames[1]] = new List<string>
                {
                    question4.Text
                }
            };

            var questionsUow = A.Fake<IQuestionsUnitOfWork>();
            var questionnairesUow = A.Fake<IQuestionnairesUnitOfWork>();
            var file = A.Fake<IFile>();

            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[0])))
                .Returns(fileText1);
            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[1])))
                .Returns(fileText2);

            var testee = new ImportExportService(questionnairesUow, questionsUow, file);

            // Act
            (Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles) = await testee.Import(name, fileNames);

            // Assert
            importedQuestions.Should().BeEquivalentTo(expectedImportedQuestions);
            erroredFiles.Should().BeNullOrEmpty();

            A.CallTo(() => questionnairesUow.QuestionnairesRepo.Add(A<Questionnaire>.That.Matches(_ => this.matchEntity(_, questionnaire))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.TextQuestionsRepo.Add(A<TextQuestion>.That.Matches(_ => this.matchEntity(_, question1))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.TextQuestionsRepo.Add(A<TextQuestion>.That.Matches(_ => this.matchEntity(_, question2))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.TextQuestionsRepo.Add(A<TextQuestion>.That.Matches(_ => this.matchEntity(_, question3))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.TextQuestionsRepo.Add(A<TextQuestion>.That.Matches(_ => this.matchEntity(_, question4))))
                .MustHaveHappened();

            A.CallTo(() => questionnairesUow.Complete())
                .MustHaveHappened();
            A.CallTo(() => questionsUow.Complete())
                .MustHaveHappened(Repeated.Exactly.Times(5));
        }

        [Fact]
        public async Task ImportsAssignmentQuestions()
        {
            // Arrange
            const string name = "assignment question";
            var fileNames = new[] { "file1", "file2" };

            var questionnaire = new Questionnaire
            {
                Revision = 1,
                Name = name
            };
            var question1 = new AssignmentQuestion
            {
                Revision = 1,
                Text = "AssignmentQuestion1",
                Options = new List<AssignmentOption>
                {
                    new AssignmentOption
                    {
                        Text = "AssignOpt1"
                    },
                    new AssignmentOption
                    {
                        Text = "AssignOpt2"
                    },
                    new AssignmentOption
                    {
                        Text = "AssignOpt3"
                    }
                }
            };
            var q1Opts = question1.Options.ToList();
            var q1Answers = (question1.Answers = new List<AssignmentAnswer>
            {
                new AssignmentAnswer
                {
                    Text = "AssignAnswer1",
                    CorrectOption = question1.Options.First()
                },
                new AssignmentAnswer
                {
                    Text = "AssignAnswer2",
                    CorrectOption = question1.Options.Skip(1).First()
                },
                new AssignmentAnswer
                {
                    Text = "AssignAnswer3",
                    CorrectOption = question1.Options.Skip(2).First()
                }
            }).ToList();
            var question2 = new AssignmentQuestion
            {
                Revision = 1,
                Text = "AssignmentQuestion2",
                Options = new List<AssignmentOption>
                {
                    new AssignmentOption
                    {
                        Text = "AssignOpt21"
                    },
                    new AssignmentOption
                    {
                        Text = "AssignOpt23"
                    }
                }
            };
            var q2Opts = question2.Options.ToList();
            var q2Answers = (question2.Answers = new List<AssignmentAnswer>
            {
                new AssignmentAnswer
                {
                    Text = "AssignAnswer21",
                    CorrectOption = question2.Options.Skip(1).First()
                },
                new AssignmentAnswer
                {
                    Text = "AssignAnswer22",
                    CorrectOption = question2.Options.First()
                }
            }).ToList();
            var question3 = new AssignmentQuestion
            {
                Revision = 1,
                Text = "AssignmentQuestion2",
                Options = new List<AssignmentOption>
                {
                    new AssignmentOption
                    {
                        Text = "AssignOpt31"
                    },
                    new AssignmentOption
                    {
                        Text = "AssignOpt32"
                    },
                    new AssignmentOption
                    {
                        Text = "AssignOpt33"
                    }
                }
            };
            var q3Opts = question3.Options.ToList();
            var q3Answers = (question3.Answers = new List<AssignmentAnswer>
            {
                new AssignmentAnswer
                {
                    Text = "AssignAnswer31",
                    CorrectOption = question3.Options.Skip(2).First()
                },
                new AssignmentAnswer
                {
                    Text = "AssignAnswer32",
                    CorrectOption = question3.Options.First()
                }
            }).ToList();
            var fileText1 = "0;\n" +
                            "Text;Opt1;Opt2;Opt3;;Assign1Text;Assign1Opt;Assign2Text;Assign2Opt;Assign3Text;Assign3Opt\n" +
                            $"\"{question1.Text}\";{q1Opts[0].Text};{q1Opts[1].Text};{q1Opts[2].Text};;{q1Answers[0].Text};1;{q1Answers[1].Text};2;{q1Answers[2].Text};3\n" +
                            $"\"{question2.Text}\";{q2Opts[0].Text};{q2Opts[1].Text};;;{q1Answers[0].Text};2;{q1Answers[1].Text};1;;";
            var fileText2 = "0;\n" +
                            "Text;Opt1;Opt2;Opt3;;Assign1Text;Assign1Opt;Assign2Text;Assign2Opt;Assign3Text;Assign3Opt\n" +
                            $"{question3.Text};{q3Opts[0].Text};{q3Opts[1].Text};{q3Opts[2].Text};;{q3Answers[0].Text};3;{q3Answers[1].Text};1;;;\n" +
                            $"";

            var expectedImportedQuestions = new Dictionary<string, List<string>>
            {
                [fileNames[0]] = new List<string>
                {
                    question1.Text,
                    question2.Text
                },
                [fileNames[1]] = new List<string>
                {
                    question3.Text
                }
            };

            var questionsUow = A.Fake<IQuestionsUnitOfWork>();
            var questionnairesUow = A.Fake<IQuestionnairesUnitOfWork>();
            var file = A.Fake<IFile>();

            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[0])))
                .Returns(fileText1);
            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[1])))
                .Returns(fileText2);

            var testee = new ImportExportService(questionnairesUow, questionsUow, file);

            // Act
            (Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles) = await testee.Import(name, fileNames);

            // Assert
            importedQuestions.Should().BeEquivalentTo(expectedImportedQuestions);
            erroredFiles.Should().BeNullOrEmpty();

            A.CallTo(() => questionnairesUow.QuestionnairesRepo.Add(A<Questionnaire>.That.Matches(_ => this.matchEntity(_, questionnaire))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.AssignmentQuestionsRepo.Add(A<AssignmentQuestion>.That.Matches(_ => this.matchEntity(_, question1))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.AssignmentQuestionsRepo.Add(A<AssignmentQuestion>.That.Matches(_ => this.matchEntity(_, question2))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.AssignmentQuestionsRepo.Add(A<AssignmentQuestion>.That.Matches(_ => this.matchEntity(_, question3))))
                .MustHaveHappened();

            A.CallTo(() => questionnairesUow.Complete())
                .MustHaveHappened();
            A.CallTo(() => questionsUow.Complete())
                .MustHaveHappened(Repeated.Exactly.Times(4));
        }

        [Fact]
        public async Task ImportsTextQuestions_WhenOneFileIsNotFound()
        {
            // Arrange
            const string name = "text question";
            var fileNames = new[] { "file1", "invalidFile" };

            var questionnaire = new Questionnaire
            {
                Revision = 1,
                Name = name
            };

            var question1 = new TextQuestion
            {
                Revision = 1,
                Text = "TextQuestion1",
                Answer = new TextAnswer
                {
                    Text = "TextAnswer1"
                }
            };
            var fileText1 = "2\n" +
                            "Text;Antwort\n" +
                            $"{question1.Text};{question1.Answer.Text}\n";

            var questionsUow = A.Fake<IQuestionsUnitOfWork>();
            var questionnairesUow = A.Fake<IQuestionnairesUnitOfWork>();
            var file = A.Fake<IFile>();

            var expectedImportedQuestions = new Dictionary<string, List<string>>
            {
                [fileNames[0]] = new List<string>
                {
                    question1.Text
                }
            };

            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[0])))
                .Returns(fileText1);
            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[1])))
                .Throws(() => new FileNotFoundException());

            var testee = new ImportExportService(questionnairesUow, questionsUow, file);

            // Act
            (Dictionary<string, List<string>> importedQuestions, List<string> erroredFiles) = await testee.Import(name, fileNames);

            // Assert
            importedQuestions.Should().BeEquivalentTo(expectedImportedQuestions);
            erroredFiles.Should().BeEquivalentTo(fileNames[1]);

            A.CallTo(() => questionnairesUow.QuestionnairesRepo.Add(A<Questionnaire>.That.Matches(_ => this.matchEntity(_, questionnaire))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.TextQuestionsRepo.Add(A<TextQuestion>.That.Matches(_ => this.matchEntity(_, question1))))
                .MustHaveHappened();

            A.CallTo(() => questionnairesUow.Complete())
                .MustHaveHappened();
            A.CallTo(() => questionsUow.Complete())
                .MustHaveHappened(Repeated.Exactly.Twice);
        }
    }
}
