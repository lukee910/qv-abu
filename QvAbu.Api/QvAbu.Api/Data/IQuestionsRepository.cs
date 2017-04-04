using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Models;

namespace QvAbu.Api.Data
{
    public interface IQuestionsRepository
    {
        Task<List<AssignmentQuestion>> GetAssignmentQuestionsAsync();
        Task<List<SimpleQuestion>> GetSimpleQuestionsAsync();
        Task<List<TextQuestion>> GetTextQuestionsAsync();
    }
}
