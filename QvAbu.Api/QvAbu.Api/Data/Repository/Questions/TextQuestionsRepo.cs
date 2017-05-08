using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data.Repository.Questions
{
    public interface ITextQuestionsRepo : IRepository<TextQuestion>
    {
    }

    public class TextQuestionsRepo
        : Repository<TextQuestion, QuestionsContext>, ITextQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public TextQuestionsRepo(QuestionsContext context) : base(context)
        {
        }

        #endregion

        #region Public Methods

        public override async Task<IEnumerable<TextQuestion>> GetAllAsync()
        {
            return await this.Context.TextQuestions
                .Include(_ => _.Answer)
                .ToListAsync();
        }

        #endregion
    }
}
