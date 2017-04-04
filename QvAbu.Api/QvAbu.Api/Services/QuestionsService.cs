using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Data;
using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Services
{
    public class QuestionsService
    {
        #region Members
        
        private IQuestionsRepository repo;

        #endregion

        #region Ctor

        public QuestionsService(IQuestionsRepository repo)
        {
            this.repo = repo;
        }

        #endregion

        #region Public Methods

        public async Task<List<Question>> GetQuestionsAsync()
        {
            var ret = new List<Question>();

            ret.AddRange(await this.repo.GetAssignmentQuestionsAsync());
            ret.AddRange(await this.repo.GetSimpleQuestionsAsync());
            ret.AddRange(await this.repo.GetTextQuestionsAsync());

            return ret;
        }

        #endregion

    }
}
