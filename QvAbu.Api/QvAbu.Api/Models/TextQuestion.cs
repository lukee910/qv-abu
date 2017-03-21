using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Models
{
    public class TextQuestion : Question
    {
        #region Properties

        public TextAnswer Answer { get; set; }

        #endregion
    }
}
