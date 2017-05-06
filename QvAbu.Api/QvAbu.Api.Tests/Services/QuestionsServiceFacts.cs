using FakeItEasy;
using QvAbu.Api.Models.Questions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api.Tests.Services
{
    public class QuestionsServiceFacts
    {
        [Fact]
        public async Task GetsAllQuestions()
        {
            // Arrange
            var assignmentQuestions = new List<AssignmentQuestion>
            {
                new AssignmentQuestion
                {
                    ID = new Guid("00000000-0000-0000-0000-000000000001")
                }
            };
            var simpleQuestions = new List<SimpleQuestion>
            {
                new SimpleQuestion
                {
                    ID = new Guid("10000000-0000-0000-0000-000000000001")
                }
            };
            var textQuestions = new List<TextQuestion>
            {
                new TextQuestion
                {
                    ID = new Guid("00000000-0000-1000-0000-000000000001")
                }
            };
            var allQuestions = new List<Question>();
            allQuestions.AddRange(assignmentQuestions);
            allQuestions.AddRange(simpleQuestions);
            allQuestions.AddRange(textQuestions);

            var uow = A.Fake<IQuestionsUnitOfWork>();
            A.CallTo(() => uow.AssignmentQuestionsRepo.GetAllAsync())
                .Returns(assignmentQuestions);
            A.CallTo(() => uow.SimpleQuestionsRepo.GetAllAsync())
                .Returns(simpleQuestions);
            A.CallTo(() => uow.TextQuestionsRepo.GetAllAsync())
                .Returns(textQuestions);

            var testee = new QuestionsService(uow);

            // Act
            var result = await testee.GetQuestionsAsync();

            // Assert
            result.ShouldAllBeEquivalentTo(allQuestions);
        }
    }
}
