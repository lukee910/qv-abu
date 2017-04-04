using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FakeItEasy;
using QvAbu.Api.Data;

namespace QvAbu.Api.Tests.Facts
{
    public class QuestionsServiceFacts
    {
        // TODO Get all questions

        public void GetsAllQuestions()
        {
            // Arrange
            var context = A.Fake<IQuestionsContext>();

            // Act

            // Assert

        }
    }
}
