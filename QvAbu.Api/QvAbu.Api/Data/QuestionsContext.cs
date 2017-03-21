using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Data
{
    public class QuestionsContext : DbContext
    {
        #region Ctor
        
        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        {

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
