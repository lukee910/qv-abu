using QvAbu.Data.Data.UnitOfWork;

namespace QvAbu.CLI
{
    class ImportExportService
    {
        #region Members

        private readonly IQuestionnairesUnitOfWork questionnairesUow;
        private readonly IQuestionsUnitOfWork questionsUow;

        #endregion

        #region Ctors

        public ImportExportService(IQuestionnairesUnitOfWork questionnairesUow, 
            IQuestionsUnitOfWork questionsUow)
        {
            this.questionnairesUow = questionnairesUow;
            this.questionsUow = questionsUow;
        }

        #endregion

        #region Props

        #endregion

        #region Methods

        public void Export()
        {
            // TODO: Export
        }

        #endregion
    }
}
