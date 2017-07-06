using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository
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
            this.IncludesFunc = queryable => queryable.Include(_ => _.Answers)
                                                      .Include(_ => _.Options);
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
