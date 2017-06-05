using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
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
        }

        #endregion

        #region Public Methods

        public override async Task<TextQuestion> GetEntityAsync(Guid id, int revision)
        {
            return await this.GetEntityAsync(id, revision, queryable => queryable.Include(_ => _.Answer));
        }

        #endregion
    }
}
