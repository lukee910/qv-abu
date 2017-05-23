using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Models.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IEnumerable<Questionnaire>> GetQuestionnaires()
        {
            return await this.service.GetQuestionnairesAsync();
        }

        #endregion
    }
}
