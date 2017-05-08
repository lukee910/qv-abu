using QvAbu.Api.Data.Repository.Questionnaire;

namespace QvAbu.Api.Data.UnitOfWork
{
    public interface IQuestionnaireUnitOfWork : IUnitOfWork
    {
        IQuestionnaireRepo QuestionnaireRepo { get; }
    }

    public class QuestionnaireUnitOfWork : UnitOfWork, IQuestionnaireUnitOfWork
    {
        #region Members

        #endregion

        #region Ctor

        public QuestionnaireUnitOfWork(QuestionsContext context, IQuestionnaireRepo repo) : base(context)
        {
            this.QuestionnaireRepo = repo;
            this.context = context;
        }

        #endregion

        #region Props

        public IQuestionnaireRepo QuestionnaireRepo { get; }

        #endregion

        #region Public Methods

        #endregion
    }
}
