using QvAbu.Api.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionnaireModel = QvAbu.Api.Models.Questionnaire.Questionnaire;

namespace QvAbu.Api.Services.Questionnaire
{
    public interface IQuestionnaireService
    {
        Task<IEnumerable<QuestionnaireModel>> GetQuestionnairesAsync();
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

        public async Task<IEnumerable<QuestionnaireModel>> GetQuestionnairesAsync()
        {
            return await this.unitOfWork.QuestionnaireRepo.GetAllAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
