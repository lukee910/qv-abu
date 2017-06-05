using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QvAbu.Api.Models.Questions;
using QvAbu.Api.Models.Questions.ReadModel;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface IQuestionnairesRepo : IRevisionEntitesRepo<Questionnaire>
    {
        Task<IEnumerable<QuestionnairePreview>> GetPreviewsAsync();
    }

    public class QuestionnairesRepo 
        : RevisionEntitesRepo<Questionnaire, IQuestionsContext>, IQuestionnairesRepo
    {
        #region Members

        #endregion

        #region Ctor

        public QuestionnairesRepo(IQuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public async Task<IEnumerable<QuestionnairePreview>> GetPreviewsAsync()
        {
            return await this.Context.Questionnaires.Select(_ => new QuestionnairePreview
            {
                ID = _.ID,
                Revision = _.Revision,
                Name = _.Name,
                QuestionsCount = _.QuestionnaireQuestions.Count()
            }).ToListAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
