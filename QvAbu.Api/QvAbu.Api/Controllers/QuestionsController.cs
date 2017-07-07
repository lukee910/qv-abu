using System;
using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        #region Members

        private readonly IQuestionsService service;

        #endregion

        #region Ctor

        public QuestionsController(IQuestionsService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        //[HttpGet("{id}/{revision}")]
        //public async Task<Question> GetQuestion(Guid id, int revision)
        //{
        //    return await this.service.GetQuestionAsync(id, revision);
        //}

        #endregion
    }
}
