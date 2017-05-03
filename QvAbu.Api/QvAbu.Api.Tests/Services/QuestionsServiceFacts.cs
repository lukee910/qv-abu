using FakeItEasy;
using QvAbu.Api.Data;
using QvAbu.Api.Models;
using QvAbu.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

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

            var repo = A.Fake<IQuestionsRepository>();
            A.CallTo(() => repo.GetAssignmentQuestions())
                .Returns(assignmentQuestions);
            A.CallTo(() => repo.GetSimpleQuestions())
                .Returns(simpleQuestions);
            A.CallTo(() => repo.GetTextQuestions())
                .Returns(textQuestions);

            var testee = new QuestionsService(repo);

            // Act
            var result = await testee.GetQuestionsAsync();

            // Assert
            result.ShouldAllBeEquivalentTo(allQuestions);
        }
    }
}
