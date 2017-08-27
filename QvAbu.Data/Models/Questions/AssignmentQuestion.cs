using System.Collections.Generic;

namespace QvAbu.Data.Models.Questions
{
    public class AssignmentQuestion : Question
    {
        #region Properties

        public List<AssignmentOption> Options { get; set; }
        public List<AssignmentAnswer> Answers { get; set; }

        #endregion
    }
}
