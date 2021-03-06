﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using QvAbu.Api.Services.Questions;
using QvAbu.Data.Models.Questions.ReadModel;
using QvAbu.Data.Models.Questions;
using QvAbu.Data.Models;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionnairesController : Controller
    {
        #region Members

        private readonly IQuestionsService service;

        #endregion

        #region Ctor

        public QuestionnairesController(IQuestionsService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        [HttpGet("previews")]
        public async Task<IEnumerable<QuestionnairePreview>> GetPreviews()
        {
            return await this.service.GetQuestionnairePreviewsAsync();
        }

        [HttpPost("questions")]
        public async Task<IEnumerable<Question>> GetQuestions([FromBody] GetQuestionsPayload payload)
        {
            return await this.service.GetQuestionsForQuestionnairesAsync(payload.Questionnaires, payload.RandomizeSeed, payload.QuestionsCount);
        }

        #endregion

        public class GetQuestionsPayload
        {
            public List<RevisionEntity> Questionnaires { get; set; }
            public int RandomizeSeed { get; set; }
            public int QuestionsCount { get; set; }
        }
    }
}
