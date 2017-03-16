using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models
{
    public class SimpleChoiceQuestion : QuestionBase
    {
        #region Properties

        public List<SimpleChoiceAnswer> Answers { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool IsNumberOfAnswersGiven { get; set; }

        #endregion
    }
}
