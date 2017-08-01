using System.Threading.Tasks;
using QvAbu.Data.Data.Repository.Questions;

namespace QvAbu.Data.Data.UnitOfWork
{
    public interface IQuestionsUnitOfWork : IUnitOfWork
    {
        IAssignmentQuestionsRepo AssignmentQuestionsRepo { get; }
        ISimpleQuestionsRepo SimpleQuestionsRepo { get; }
        ITextQuestionsRepo TextQuestionsRepo { get; }
    }

    public class QuestionsUnitOfWork : IQuestionsUnitOfWork
    {
        #region Members

        private readonly QuestionsContext context;

        #endregion

        #region Ctor

        public QuestionsUnitOfWork(QuestionsContext context,
            IAssignmentQuestionsRepo assignmentQuestionsRepo,
            ISimpleQuestionsRepo simpleQuestionsRepo,
            ITextQuestionsRepo textQuestionsRepo)
        {
            this.context = context;

            this.AssignmentQuestionsRepo = assignmentQuestionsRepo;
            this.SimpleQuestionsRepo = simpleQuestionsRepo;
            this.TextQuestionsRepo = textQuestionsRepo;
        }

        #endregion

        #region Props

        public IAssignmentQuestionsRepo AssignmentQuestionsRepo { get; }
        public ISimpleQuestionsRepo SimpleQuestionsRepo { get; }
        public ITextQuestionsRepo TextQuestionsRepo { get; }

        #endregion

        #region Public Methods

        public async Task<int> Complete()
        {
            return await this.context.SaveChangesAsync();
        }

        #endregion
    }
}
