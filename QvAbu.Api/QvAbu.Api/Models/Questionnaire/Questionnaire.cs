using QvAbu.Api.Models.Glue;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;

namespace QvAbu.Api.Models.Questionnaire
{
    public class Questionnaire : RevisionEntity
    {
        public string Name { get; set; }
        
        public virtual IEnumerable<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
    }
}
