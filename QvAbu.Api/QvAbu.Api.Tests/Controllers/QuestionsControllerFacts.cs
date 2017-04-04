using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using QvAbu.Api.Controllers;
using QvAbu.Api.Models;
using QvAbu.Api.Services;
using Xunit;

namespace QvAbu.Api.Tests.Controllers
{
    public class QuestionsControllerFacts
    {
        [Fact]
        public async Task GetsAllQuestions()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question
                {
                    ID = new Guid("00000000-1000-0000-0000-000000000001")
                }
            };
            var service = A.Fake<IQuestionsService>();
            A.CallTo(() => service.GetQuestionsAsync())
                .Returns(questions);

            var testee = new QuestionsController(service);

            // Act
            var result = await testee.GetQuestions();

            // Assert
            result.Should().BeSameAs(questions);
        }
    }
}
