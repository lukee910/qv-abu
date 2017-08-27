using System.Collections.Generic;

namespace QvAbu.Data.Models.Questions
{
    public class SimpleQuestion : Question
    {
        #region Properties

        public IList<SimpleAnswer> Answers { get; set; }
        public SimpleQuestionType SimpleQuestionType { get; set; }

        #endregion
    }

    public enum SimpleQuestionType
    {
        SingleChoice = 0,
        MultipleChoice = 1,
        TrueFalse = 2
    }
}
