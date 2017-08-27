using System;

namespace QvAbu.Data.Models.Questions
{
    public class TextQuestion : Question
    {
        #region Properties

        public Guid AnswerId { get; set; }

        public virtual TextAnswer Answer { get; set; }

        #endregion
    }
}
