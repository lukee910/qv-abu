using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;
using QvAbu.Api.Models.Questionnaire;
using QvAbu.Api.Models.Questions;

namespace QvAbu.Api.Data
{
    public class QuestionsContext : DbContext
    {
        #region Ctor

        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.SetRevisionEntityKey<SimpleQuestion>();
            //modelBuilder.SetRevisionEntityKey<AssignmentQuestion>();
            //modelBuilder.SetRevisionEntityKey<TextQuestion>();
            modelBuilder.SetRevisionEntityKey<Question>();
            modelBuilder.SetRevisionEntityKey<Questionnaire>();
        }

        private void SetQuestionKey<T>(ModelBuilder modelBuilder) where T : RevisionEntity
        {
            modelBuilder.Entity<T>()
                .HasKey(_ => new { _.ID, _.Revision });
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

        #endregion
    }
}
