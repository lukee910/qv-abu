using System.Collections.Generic;

namespace QvAbu.Data.Models.Questions
{
    public class Questionnaire : RevisionEntity
    {
        public string Name { get; set; }
        
        public virtual IEnumerable<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
    }
}
