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

        private QuestionsContext temp;

        #endregion

        #region Ctor

        public QuestionsController(QuestionsContext context)
        {
            this.temp = context;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ICollection<Question> GetQuestions()
        {
            return this.temp.SimpleQuestions.ToArray();
        }

        #endregion
    }
}
