using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;

namespace QvAbu.Api.Data.Repository
{
    internal interface IAssignmentQuestionsRepo : IRepository<AssignmentQuestion>
    {
    }

    internal class AssignmentQuestionsRepo 
        : Repository<AssignmentQuestion, QuestionsContext>, IAssignmentQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public AssignmentQuestionsRepo(QuestionsContext context) : base(context)
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
