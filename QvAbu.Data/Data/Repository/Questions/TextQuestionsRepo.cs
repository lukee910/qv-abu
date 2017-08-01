using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Data.Data.Repository.Questions
{
    public interface ITextQuestionsRepo : IRevisionEntitesRepo<TextQuestion>
    {
    }

    public class TextQuestionsRepo
        : RevisionEntitesRepo<TextQuestion, IQuestionsContext>, ITextQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public TextQuestionsRepo(IQuestionsContext context) : base(context)
        {
            this.IncludesFunc = queryable => queryable.Include(_ => _.Answer);
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
