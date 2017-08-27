using System;

namespace QvAbu.Data.Models.Questions
{
    public class QuestionnaireQuestion
    {
        public Guid ID { get; set; }

        public Questionnaire Questionnaire { get; set; }
        public Question Question { get; set; }
    }
}
