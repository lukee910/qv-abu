using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models.Questions
{
    public class TextQuestion : Question
    {
        #region Properties

        public Guid AnswerId { get; set; }

        public virtual TextAnswer Answer { get; set; }

        #endregion
    }
}
