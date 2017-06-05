using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
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
        }

        #endregion

        #region Public Methods

        public override async Task<SimpleQuestion> GetEntityAsync(Guid id, int revision)
        {
            return await this.GetEntityAsync(id, revision, queryable => queryable.Include(_ => _.Answers));
        }

        #endregion
    }
}
