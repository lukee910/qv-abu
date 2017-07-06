using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository
{
    public interface ITextQuestionsRepo : IRevisionEntitesRepo<TextQuestion>
    {
    }

    public class TextQuestionsRepo
        : RevisionEntitesRepo<TextQuestion, IQuestionsContext>, ITextQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public TextQuestionsRepo(IQuestionsContext context) : base(context)
        {
            this.IncludesFunc = queryable => queryable.Include(_ => _.Answer);
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
