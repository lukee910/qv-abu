using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionnaireService
    {
        Task<IEnumerable<Questionnaire>> GetQuestionnairesAsync();
    }

    public class QuestionnaireService : IQuestionnaireService
    {
        #region Members

        private IQuestionnaireUnitOfWork unitOfWork;

        #endregion

        #region Ctor

        public QuestionnaireService(IQuestionnaireUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Props

        #endregion

        #region Public Methods

        public async Task<IEnumerable<Questionnaire>> GetQuestionnairesAsync()
        {
            return await this.unitOfWork.QuestionnaireRepo.GetAllAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
