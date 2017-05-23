using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface IQuestionnaireRepo : IRepository<Questionnaire>
    {
    }

    public class QuestionnaireRepo 
        : Repository<Questionnaire, QuestionsContext>, IQuestionnaireRepo
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

        public async override Task<IEnumerable<Questionnaire>> GetAllAsync()
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
