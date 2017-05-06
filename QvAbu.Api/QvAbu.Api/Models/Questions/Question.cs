using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models.Questions
{
    public class Question : Entity
    {
        #region Properties

        public int Revision { get; set; }
        public string Text { get; set; }

        #endregion
    }
}
