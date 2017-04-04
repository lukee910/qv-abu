using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;

namespace QvAbu.Api.Data
{
    public class QuestionsRepository : IQuestionsRepository
    {
        #region Members

        private readonly IQuestionsContext context;

        #endregion

        #region Ctor

        public QuestionsRepository(IQuestionsContext context)
        {
            this.context = context;
        }

        #endregion

        #region Public Methods

        public async Task<List<AssignmentQuestion>> GetAssignmentQuestionsAsync()
        {
            return await this.context.AssignmentQuestions.ToListAsync();
        }

        public async Task<List<SimpleQuestion>> GetSimpleQuestionsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TextQuestion>> GetTextQuestionsAsync()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}