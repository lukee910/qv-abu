using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface ISimpleQuestionsRepo : IRepository<SimpleQuestion>
    {
    }

    public class SimpleQuestionsRepo 
        : Repository<SimpleQuestion, QuestionsContext>, ISimpleQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public SimpleQuestionsRepo(QuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Public Methods

        public override async Task<IEnumerable<SimpleQuestion>> GetAllAsync()
        {
            return await this.Context.SimpleQuestions
                .Include(_ => _.Answers)
                .ToListAsync();
        }

        #endregion
    }
}
