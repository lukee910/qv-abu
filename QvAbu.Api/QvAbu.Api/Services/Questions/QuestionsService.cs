using System;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Data.Repository;
using QvAbu.Api.Data.UnitOfWork;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionsService
    {
        Task<Question> GetQuestionAsync(Guid id, int revision);
    }

    public class QuestionsService : IQuestionsService
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

        public async Task<Question> GetQuestionAsync(Guid id, int revision)
        {
            foreach (var repo in new IRevisionEntitesRepo[]
            {
                this.unitOfWork.AssignmentQuestionsRepo,
                this.unitOfWork.SimpleQuestionsRepo,
                this.unitOfWork.TextQuestionsRepo
            })
            {
                var ret = await repo.GetAsync(id, revision);
                if (ret != null)
                {
                    return (Question) ret;
                }
            }

            return null;
        }

        #endregion
    }
}
