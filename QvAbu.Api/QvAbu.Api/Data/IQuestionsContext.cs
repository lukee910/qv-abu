using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;

namespace QvAbu.Api.Data
{
    public interface IQuestionsContext
    {
        DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
        DbSet<AssignmentOption> AssignmentOptions { get; set; }
        DbSet<AssignmentQuestion> AssignmentQuestions { get; set; }
        DbSet<SimpleAnswer> SimpleAnswers { get; set; }
        DbSet<SimpleQuestion> SimpleQuestions { get; set; }
        DbSet<TextAnswer> TextAnswers { get; set; }
        DbSet<TextQuestion> TextQuestions { get; set; }
    }
}