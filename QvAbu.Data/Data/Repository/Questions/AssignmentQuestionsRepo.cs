using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Data.Data.Repository.Questions
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
