using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface IAssignmentQuestionsRepo : IRevisionEntitesRepo<AssignmentQuestion>
    {
    }

    public class AssignmentQuestionsRepo 
        : RevisionEntitesRepo<AssignmentQuestion, IQuestionsContext>, IAssignmentQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public AssignmentQuestionsRepo(IQuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Public Methods

        public override async Task<AssignmentQuestion> GetEntityAsync(Guid id, int revision)
        {
            return await this.GetEntityAsync(id, revision, queryable =>
            {
                return queryable.Include(_ => _.Answers)
                    .Include(_ => _.Options);
            });
        }

        #endregion
    }
}
