using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models
{
    public class AssignmentAnswer : Answer
    {
        #region Properties

        public AssignmentOption CorrectOption { get; set; }

        #endregion
    }
}
