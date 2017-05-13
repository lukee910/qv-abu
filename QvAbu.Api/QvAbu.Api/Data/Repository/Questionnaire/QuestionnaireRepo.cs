using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models.Questionnaire;
using QuestionnaireModel = QvAbu.Api.Models.Questionnaire.Questionnaire;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QvAbu.Api.Data.Repository.Questionnaire
{
    public interface IQuestionnaireRepo : IRepository<QuestionnaireModel>
    {
    }

    public class QuestionnaireRepo 
        : Repository<QuestionnaireModel, QuestionsContext>, IQuestionnaireRepo
    {
        #region Members

        #endregion

        #region Ctor

        public QuestionnaireRepo(QuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public async override Task<IEnumerable<QuestionnaireModel>> GetAllAsync()
        {
            var result = await this.Context.Questionnaires
                .Include(_ => _.QuestionnaireQuestions)
                    .ThenInclude(_ => _.Question)
                .ToListAsync();

            result.ForEach(q => q.QuestionnaireQuestions
                                 .ToList()
                                 .ForEach(qq => 
                                 {
                                     qq.Questionnaire = null;
                                     qq.Question.QuestionnaireQuestions = null;
                                 }));

            return result;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
