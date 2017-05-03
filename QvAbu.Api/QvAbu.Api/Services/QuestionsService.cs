using QvAbu.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Data.UnitOfWork;

namespace QvAbu.Api.Services
{
    internal class QuestionsService : IQuestionsService
    {
        #region Members
        
        private readonly IQuestionsUnitOfWork unitOfWork;

        #endregion

        #region Ctor

        public QuestionsService(IQuestionsUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        public async Task<List<Question>> GetQuestionsAsync()
        {
            var ret = new List<Question>();

            ret.AddRange(await this.unitOfWork.AssignmentQuestionsRepo.GetAllAsync());
            ret.AddRange(await this.unitOfWork.SimpleQuestionsRepo.GetAllAsync());
            ret.AddRange(await this.unitOfWork.TextQuestionsRepo.GetAllAsync());

            return ret;
        }

        #endregion

    }
}
