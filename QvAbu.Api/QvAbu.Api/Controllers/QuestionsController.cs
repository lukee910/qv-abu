using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Services;
using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ICollection<Question>> GetQuestions()
        {
            return await this.service.GetQuestionsAsync();
        }

        #endregion
    }
}
