using QvAbu.Api.Data.Repository.Questions;

namespace QvAbu.Api.Data.UnitOfWork
{
    public interface IQuestionnairesUnitOfWork : IUnitOfWork
    {
        IQuestionnairesRepo QuestionnairesRepo { get; }
    }

    public class QuestionnairesUnitOfWork : UnitOfWork, IQuestionnairesUnitOfWork
    {
        #region Members

        #endregion

        #region Ctor

        public QuestionnairesUnitOfWork(QuestionsContext context, IQuestionnairesRepo repo) : base(context)
        {
            this.QuestionnairesRepo = repo;
            this.context = context;
        }

        #endregion

        #region Props

        public IQuestionnairesRepo QuestionnairesRepo { get; }

        #endregion

        #region Public Methods

        #endregion
    }
}
