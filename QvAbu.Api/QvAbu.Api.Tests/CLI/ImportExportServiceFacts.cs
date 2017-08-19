using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using QvAbu.CLI;
using QvAbu.CLI.Wrappers;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models.Questions;
using Xunit;

namespace QvAbu.Api.Tests.CLI
{
    public class ImportExportServiceFacts
    {
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
            var fileText = "1\n" +
                "Text;Typ;Text Antwort 1;Anwort korrekt?;Text Antwort 2;Antwort korrekt?\n" +
                $"{question1.Text};{(int)question1.SimpleQuestionType};{answers1[0].Text};{answers1[0].IsCorrect};{answers1[1].Text};{answers1[1].IsCorrect}\n" +
                $"{question2.Text};{(int)question2.SimpleQuestionType};{answers2[0].Text};{answers2[0].IsCorrect};{answers2[1].Text};{answers2[1].IsCorrect};{answers2[2].Text};{answers2[2].IsCorrect}\n";
            
            var questionsUow = A.Fake<IQuestionsUnitOfWork>();
            var questionnairesUow = A.Fake<IQuestionnairesUnitOfWork>();
            var file = A.Fake<IFile>();

            A.CallTo(() => file.ReadAllText(A<string>.That.Matches(_ => _ == fileNames[0])))
                .Returns(fileText);

            var testee = new ImportExportService(questionnairesUow, questionsUow, file);

            // Act
            (int importedQuestions, List<string> erroredFiles) = await testee.Import(name, fileNames);

            // Assert
            importedQuestions.Should().Be(2);
            erroredFiles.Should().BeNullOrEmpty();

            A.CallTo(() => questionnairesUow.QuestionnairesRepo.Add(A<Questionnaire>.That.Matches(_ => _.Equals(questionnaire))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.SimpleQuestionsRepo.Add(A<SimpleQuestion>.That.Matches(_ => _.Equals(question1))))
                .MustHaveHappened();
            A.CallTo(() => questionsUow.SimpleQuestionsRepo.Add(A<SimpleQuestion>.That.Matches(_ => _.Equals(question2))))
                .MustHaveHappened();

            A.CallTo(() => questionnairesUow.Complete())
                .MustNotHaveHappened();
            A.CallTo(() => questionsUow.Complete())
                .MustHaveHappened(Repeated.Exactly.Twice);
        }
    }
}
