using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models
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
