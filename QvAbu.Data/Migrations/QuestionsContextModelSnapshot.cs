﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using QvAbu.Data.Data;
using QvAbu.Data.Models.Questions;
using System;

namespace QvAbu.Data.Migrations
{
    [DbContext(typeof(QuestionsContext))]
    partial class QuestionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QvAbu.Data.Models.Questions.AssignmentAnswer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssignmentQuestionID");

                    b.Property<int?>("AssignmentQuestionRevision");

                    b.Property<Guid>("CorrectOptionId");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("CorrectOptionId");

                    b.HasIndex("AssignmentQuestionID", "AssignmentQuestionRevision");

                    b.ToTable("AssignmentAnswers");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.AssignmentOption", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssignmentQuestionID");

                    b.Property<int?>("AssignmentQuestionRevision");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentQuestionID", "AssignmentQuestionRevision");

                    b.ToTable("AssignmentOptions");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.Question", b =>
                {
                    b.Property<Guid>("ID");

                    b.Property<int>("Revision");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Text");

                    b.HasKey("ID", "Revision");

                    b.ToTable("Question");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Question");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.Questionnaire", b =>
                {
                    b.Property<Guid>("ID");

                    b.Property<int>("Revision");

                    b.Property<string>("Name");

                    b.Property<string>("Tags");

                    b.HasKey("ID", "Revision");

                    b.ToTable("Questionnaires");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.QuestionnaireQuestion", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("QuestionID");

                    b.Property<int?>("QuestionRevision");

                    b.Property<Guid?>("QuestionnaireID");

                    b.Property<int?>("QuestionnaireRevision");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID", "QuestionRevision");

                    b.HasIndex("QuestionnaireID", "QuestionnaireRevision");

                    b.ToTable("QuestionnaireQuestions");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.SimpleAnswer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCorrect");

                    b.Property<Guid?>("SimpleQuestionID");

                    b.Property<int?>("SimpleQuestionRevision");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("SimpleQuestionID", "SimpleQuestionRevision");

                    b.ToTable("SimpleAnswers");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.TextAnswer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.ToTable("TextAnswers");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.AssignmentQuestion", b =>
                {
                    b.HasBaseType("QvAbu.Data.Models.Questions.Question");


                    b.ToTable("AssignmentQuestion");

                    b.HasDiscriminator().HasValue("AssignmentQuestion");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.SimpleQuestion", b =>
                {
                    b.HasBaseType("QvAbu.Data.Models.Questions.Question");

                    b.Property<int>("SimpleQuestionType");

                    b.ToTable("SimpleQuestion");

                    b.HasDiscriminator().HasValue("SimpleQuestion");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.TextQuestion", b =>
                {
                    b.HasBaseType("QvAbu.Data.Models.Questions.Question");

                    b.Property<Guid>("AnswerId");

                    b.HasIndex("AnswerId");

                    b.ToTable("TextQuestion");

                    b.HasDiscriminator().HasValue("TextQuestion");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.AssignmentAnswer", b =>
                {
                    b.HasOne("QvAbu.Data.Models.Questions.AssignmentOption", "CorrectOption")
                        .WithMany()
                        .HasForeignKey("CorrectOptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QvAbu.Data.Models.Questions.AssignmentQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("AssignmentQuestionID", "AssignmentQuestionRevision");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.AssignmentOption", b =>
                {
                    b.HasOne("QvAbu.Data.Models.Questions.AssignmentQuestion")
                        .WithMany("Options")
                        .HasForeignKey("AssignmentQuestionID", "AssignmentQuestionRevision");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.QuestionnaireQuestion", b =>
                {
                    b.HasOne("QvAbu.Data.Models.Questions.Question", "Question")
                        .WithMany("QuestionnaireQuestions")
                        .HasForeignKey("QuestionID", "QuestionRevision");

                    b.HasOne("QvAbu.Data.Models.Questions.Questionnaire", "Questionnaire")
                        .WithMany("QuestionnaireQuestions")
                        .HasForeignKey("QuestionnaireID", "QuestionnaireRevision");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.SimpleAnswer", b =>
                {
                    b.HasOne("QvAbu.Data.Models.Questions.SimpleQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("SimpleQuestionID", "SimpleQuestionRevision");
                });

            modelBuilder.Entity("QvAbu.Data.Models.Questions.TextQuestion", b =>
                {
                    b.HasOne("QvAbu.Data.Models.Questions.TextAnswer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
