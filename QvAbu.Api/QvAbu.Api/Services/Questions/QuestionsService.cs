using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models.Questions;
using QvAbu.Data.Models.Questions.ReadModel;
using QvAbu.Data.Data.Repository;
using QvAbu.Data.Models;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync();
        Task<IEnumerable<Question>> GetQuestionsForQuestionnairesAsync(List<RevisionEntity> questionnaires, 
            int randomizeSeed = 0, 
            int questionsCount = 0);
    }

    public class QuestionsService : IQuestionsService
    {
        #region Members
        
        private readonly IQuestionsUnitOfWork questionsUow;
        private readonly IQuestionnairesUnitOfWork questionnairesUow;

        #endregion

        #region Ctor

        public QuestionsService(IQuestionsUnitOfWork questionsUow, IQuestionnairesUnitOfWork questionnairesUow)
        {
            this.questionsUow = questionsUow;
            this.questionnairesUow = questionnairesUow;
        }

        #endregion

        #region Public Methods
        
        public async Task<IEnumerable<QuestionnairePreview>> GetQuestionnairePreviewsAsync()
        {
            return await this.questionnairesUow.QuestionnairesRepo.GetPreviewsAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsForQuestionnairesAsync(List<RevisionEntity> questionnaires,
            int randomizeSeed = 0,
            int questionsCount = 0)
        {
            var ret = new List<Question>();
            foreach(var questionnaire in questionnaires)
            {
                var keys = (await this.questionnairesUow.QuestionnairesRepo.GetQuestionKeysAsync(questionnaire.ID, questionnaire.Revision)).ToList();

                foreach (var repo in new IRevisionEntitesRepo[]
                {
                    this.questionsUow.AssignmentQuestionsRepo,
                    this.questionsUow.SimpleQuestionsRepo,
                    this.questionsUow.TextQuestionsRepo
                })
                {
                    ret.AddRange((IEnumerable<Question>)await repo.GetListAsync(keys));
                }
            }

            if (randomizeSeed != 0)
            {
                var rand = new Random(randomizeSeed);
                ret = ret.OrderBy(_ => rand.Next())
                    .ToList();
            }

            if (questionsCount > 0)
            {
                ret = ret.Take(questionsCount)
                    .ToList();
            }

            return ret;
        }

        #endregion
    }
}
