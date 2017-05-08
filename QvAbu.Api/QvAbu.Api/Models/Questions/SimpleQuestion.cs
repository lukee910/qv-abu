using System.Collections.Generic;

namespace QvAbu.Api.Models.Questions
{
    public class SimpleQuestion : Question
    {
        #region Properties

        public ICollection<SimpleAnswer> Answers { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool IsNumberOfAnswersGiven { get; set; }

        #endregion
    }
}
