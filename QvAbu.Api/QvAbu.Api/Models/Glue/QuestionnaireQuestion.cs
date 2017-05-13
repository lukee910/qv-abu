using QvAbu.Api.Models.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using QuestionnaireModel = QvAbu.Api.Models.Questionnaire.Questionnaire;

namespace QvAbu.Api.Models.Glue
{
    public class QuestionnaireQuestion
    {
        public Guid ID { get; set; }

        public QuestionnaireModel Questionnaire { get; set; }
        public Question Question { get; set; }
    }
}
