using Microsoft.AspNetCore.Mvc;
using QvAbu.Api.Data;
using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        #region Members

        #endregion

        #region Ctor

        public QuestionsController(QuestionsContext context)
        {

        }

        #endregion

        #region Methods

        [HttpGet]
        public ICollection<Question> GetQuestions()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
