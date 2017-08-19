using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Data.Data.Repository.Questions
{
    public interface ISimpleQuestionsRepo : IRevisionEntitesRepo<SimpleQuestion>
    {
    }

    public class SimpleQuestionsRepo 
        : RevisionEntitesRepo<SimpleQuestion, IQuestionsContext>, ISimpleQuestionsRepo
    {
        #region Members

        #endregion

        #region Ctor

        public SimpleQuestionsRepo(IQuestionsContext context) : base(context)
        {
            this.IncludesFunc = queryable => queryable.Include(_ => _.Answers);
        }

        #endregion

        #region Public Methods

        public override void Add(SimpleQuestion entity)
        {
            this.Context.SimpleQuestions.Add(entity);
        }

        #endregion
    }
}
