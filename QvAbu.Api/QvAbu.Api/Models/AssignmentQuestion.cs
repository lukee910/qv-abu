using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models
{
    public class AssignmentQuestion : QuestionBase
    {
        #region Properties

        public List<AssignmentOption> Options { get; set; }
        public List<AssignmentAnswer> Answers { get; set; }

        #endregion
    }
}
