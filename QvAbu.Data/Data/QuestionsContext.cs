using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Data.Data
{
    public interface IQuestionsContext : IDbContext
    {
        DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
        DbSet<AssignmentOption> AssignmentOptions { get; set; }
        DbSet<AssignmentQuestion> AssignmentQuestions { get; set; }
        DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
        DbSet<Questionnaire> Questionnaires { get; set; }
        DbSet<SimpleAnswer> SimpleAnswers { get; set; }
        DbSet<SimpleQuestion> SimpleQuestions { get; set; }
        DbSet<TextAnswer> TextAnswers { get; set; }
        DbSet<TextQuestion> TextQuestions { get; set; }
    }

    public class QuestionsContext : DbContext, IQuestionsContext
    {
        #region Ctor

        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SetRevisionEntityKey<Question>();
            modelBuilder.SetRevisionEntityKey<Questionnaire>();
        }

        #endregion

        #region Properties

        public DbSet<SimpleQuestion> SimpleQuestions { get; set; }
        public DbSet<SimpleAnswer> SimpleAnswers { get; set; }
        public DbSet<AssignmentQuestion> AssignmentQuestions { get; set; }
        public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
        public DbSet<AssignmentOption> AssignmentOptions { get; set; }
        public DbSet<TextQuestion> TextQuestions { get; set; }
        public DbSet<TextAnswer> TextAnswers { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }

        #endregion
    }
}
