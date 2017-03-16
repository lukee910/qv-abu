using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Services
{
    interface IQuestionsService
    {
        List<SimpleChoiceQuestion> GetSimpleQuestions();
    }
}
