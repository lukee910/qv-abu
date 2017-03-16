using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Models;
using QvAbu.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private IQuestionsService QuestionsService { get; set; }

        public QuestionsController()
        {
            this.QuestionsService = new QuestionsService();
        }

        // GET api/questions/SimpleQuestions
        [HttpGet("SimpleQuestions")]
        public IEnumerable<QuestionBase> GetSimpleChoiceQuestion()
        {
            return this.QuestionsService.GetSimpleQuestions();
        }
    }
}
