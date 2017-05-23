﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface ISimpleQuestionsRepo : IRepository<SimpleQuestion>
    {
    }

    public class SimpleQuestionsRepo 
        : Repository<SimpleQuestion, IQuestionsContext>, ISimpleQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public SimpleQuestionsRepo(IQuestionsContext context) : base(context)
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
