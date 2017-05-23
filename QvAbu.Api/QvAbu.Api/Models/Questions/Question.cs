using System.Collections.Generic;

namespace QvAbu.Api.Models.Questions
{
    public class Question : RevisionEntity
    {
        #region Properties

        public string Text { get; set; }

        public virtual IEnumerable<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }

        #endregion
    }
}
