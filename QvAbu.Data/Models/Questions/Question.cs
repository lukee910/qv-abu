using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QvAbu.Data.Models.Questions
{
    public abstract class Question : RevisionEntity
    {
        #region Properties

        public string Text { get; set; }

        [NotMapped]
        public QuestionType Type
        {
            get
            {
                var type = this.GetType();
                if (type == typeof(AssignmentQuestion))
                {
                    return QuestionType.AssignmentQuestion;
                }
                if (type == typeof(SimpleQuestion))
                {
                    return QuestionType.SimpleQuestion;
                }
                return QuestionType.TextQuestion;
            }
        }

        public virtual IEnumerable<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }

        #endregion
    }

    public enum QuestionType
    {
        AssignmentQuestion = 0,
        SimpleQuestion = 1,
        TextQuestion = 2
    }
}
