using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface IAssignmentQuestionsRepo : IRepository<AssignmentQuestion>
    {
    }

    public class AssignmentQuestionsRepo 
        : Repository<AssignmentQuestion, IQuestionsContext>, IAssignmentQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public AssignmentQuestionsRepo(IQuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Public Methods

        public override async Task<IEnumerable<AssignmentQuestion>> GetAllAsync()
        {
            return await this.Context.AssignmentQuestions
                .Include(_ => _.Options)
                .Include(_ => _.Answers)
                .ToListAsync();
        }

        #endregion
    }
}
