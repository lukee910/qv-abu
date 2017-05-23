using System;

namespace QvAbu.Api.Models.Questions
{
    public class QuestionnaireQuestion
    {
        public Guid ID { get; set; }

        public Questionnaire Questionnaire { get; set; }
        public Question Question { get; set; }
    }
}
