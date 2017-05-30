using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models.Questions.ReadModel;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionnairesController : Controller
    {
        #region Members

        private readonly IQuestionnairesService service;

        #endregion

        #region Ctor

        public QuestionnairesController(IQuestionnairesService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        [HttpGet("previews")]
        public async Task<IEnumerable<QuestionnairePreview>> GetPreviews()
        {
            var result = await this.service.GetQuestionnairePreviewsAsync();
            return result;
        }

        #endregion
    }
}
