using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Data
{
    public class QuestionsContext : DbContext
    {
        #region Ctor

        public QuestionsContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.SetQuestionKey<SimpleQuestion>(modelBuilder);
            this.SetQuestionKey<AssignmentQuestion>(modelBuilder);
            this.SetQuestionKey<TextQuestion>(modelBuilder);
        }

        private void SetQuestionKey<T>(ModelBuilder modelBuilder) where T : Question
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

        #endregion
    }
}
