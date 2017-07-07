using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository
{
    public interface ISimpleQuestionsRepo : IRevisionEntitesRepo<SimpleQuestion>
    {
    }

    public class SimpleQuestionsRepo 
        : RevisionEntitesRepo<SimpleQuestion, IQuestionsContext>, ISimpleQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public SimpleQuestionsRepo(IQuestionsContext context) : base(context)
        {
            this.IncludesFunc = queryable => queryable.Include(_ => _.Answers);
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
