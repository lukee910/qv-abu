using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models;

namespace QvAbu.Api.Services
{
    public interface IQuestionsService
    {
        Task<List<Question>> GetQuestionsAsync();
    }
}