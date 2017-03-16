using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Services
{
    public class QuestionsService : IQuestionsService
    {
        #region Members

        private List<SimpleChoiceQuestion> SimpleQuestions { get; set; }

        #endregion

        #region Ctor

        public QuestionsService()
        {
            SimpleQuestions = new List<SimpleChoiceQuestion>
            {
                new SimpleChoiceQuestion
                {
                    Text = "What's the answer to life and everything?",
                    IsMultipleChoice = false,
                    Answers = new List<SimpleChoiceAnswer>
                    {
                        new SimpleChoiceAnswer
                        {
                             Text = "Having an impact",
                             IsCorrect = false
                        },
                        new SimpleChoiceAnswer
                        {
                             Text = "42",
                             IsCorrect = true
                        },
                        new SimpleChoiceAnswer
                        {
                             Text = "True love",
                             IsCorrect = false
                        }
                    }
                }
            };
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public List<SimpleChoiceQuestion> GetSimpleQuestions()
        {
            return SimpleQuestions;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
