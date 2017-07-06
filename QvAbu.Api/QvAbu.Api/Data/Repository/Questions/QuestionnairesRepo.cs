using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QvAbu.Api.Models.Questions;
using QvAbu.Api.Models.Questions.ReadModel;

namespace QvAbu.Api.Data.Repository
{
    public interface IQuestionnairesRepo : IRevisionEntitesRepo<Questionnaire>
    {
        Task<IEnumerable<QuestionnairePreview>> GetPreviewsAsync();
        Task<IEnumerable<(Guid id, int revision)>> GetQuestionKeysAsync(Guid id, int revision);
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

        public async Task<IEnumerable<(Guid id, int revision)>> GetQuestionKeysAsync(Guid id, int revision)
        {
            return (await this.Context.QuestionnaireQuestions
                .Where(_ => _.Questionnaire.ID == id && _.Questionnaire.Revision == revision)
                .Select(_ => new Tuple<Guid, int>(_.Question.ID, _.Question.Revision))
                .ToListAsync())
                .Select(tuple => (tuple.Item1, tuple.Item2));
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
