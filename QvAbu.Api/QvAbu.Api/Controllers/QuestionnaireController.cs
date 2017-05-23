using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionnaireController : Controller
    {
        #region Members

        private readonly IQuestionnaireService service;

        #endregion

        #region Ctor

        public QuestionnaireController(IQuestionnaireService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IEnumerable<Questionnaire>> GetQuestionnaires()
        {
            return await this.service.GetQuestionnairesAsync();
        }

        #endregion
    }
}
