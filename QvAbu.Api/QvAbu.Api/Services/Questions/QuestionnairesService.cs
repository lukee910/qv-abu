using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models.Questions.ReadModel;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionnairesService
    {
        Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync();
    }

    public class QuestionnairesService : IQuestionnairesService
    {
        #region Members

        private IQuestionnairesUnitOfWork unitOfWork;

        #endregion

        #region Ctor

        public QuestionnairesService(IQuestionnairesUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Props

        #endregion

        #region Public Methods

        public async Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync()
        {
            return await this.unitOfWork.QuestionnairesRepo.GetPreviewsAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
