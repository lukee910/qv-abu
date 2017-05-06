using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Services.Questions
{
    public interface IQuestionsService
    {
        Task<List<Question>> GetQuestionsAsync();
    }
}