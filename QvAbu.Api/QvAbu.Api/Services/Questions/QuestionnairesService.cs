using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionnairesService
    {
        Task<IEnumerable<Questionnaire>> GetQuestionnairesAsync();
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

        public async Task<IEnumerable<Questionnaire>> GetQuestionnairesAsync()
        {
            return await this.unitOfWork.QuestionnairesRepo.GetAllAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
