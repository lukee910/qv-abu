using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QvAbu.Api.Models.Questions;
using QvAbu.Api.Services;

namespace QvAbu.Api.Data
{
    public class QuestionsContext : DbContext
    {
        #region Ctor

        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        {
            var serviceProvider = this.GetInfrastructure();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new LoggerProvider());
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
