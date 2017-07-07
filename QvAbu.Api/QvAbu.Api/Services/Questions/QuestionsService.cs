using System;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QvAbu.Api.Data.Repository;
using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Models.Questions.ReadModel;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync();
        Task<IEnumerable<Question>> GetQuestionsForQuestionnaireAsync(Guid id, int revision);
    }

    public class QuestionsService : IQuestionsService
    {
        #region Members
        
        private readonly IQuestionsUnitOfWork questionsUow;
        private readonly IQuestionnairesUnitOfWork questionnairesUow;

        #endregion

        #region Ctor

        public QuestionsService(IQuestionsUnitOfWork questionsUow, IQuestionnairesUnitOfWork questionnairesUow)
        {
            this.questionsUow = questionsUow;
            this.questionnairesUow = questionnairesUow;
        }

        #endregion

        #region Public Methods
        
        public async Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync()
        {
            return await this.questionnairesUow.QuestionnairesRepo.GetPreviewsAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsForQuestionnaireAsync(Guid id, int revision)
        {
            var keys = (await this.questionnairesUow.QuestionnairesRepo.GetQuestionKeysAsync(id, revision)).ToList();
            var ret = new List<Question>();

            foreach (var repo in new IRevisionEntitesRepo[]
            {
                this.questionsUow.AssignmentQuestionsRepo,
                this.questionsUow.SimpleQuestionsRepo,
                this.questionsUow.TextQuestionsRepo
            })
            {
                ret.AddRange((IEnumerable<Question>) await repo.GetListAsync(keys));
            }

            return ret;
        }

        #endregion
    }
}
