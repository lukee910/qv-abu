using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QvAbu.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => new { x.ID, x.Revision });
                });

            migrationBuilder.CreateTable(
                name: "TextAnswers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnswers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    SimpleQuestionType = table.Column<int>(nullable: true),
                    AnswerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => new { x.ID, x.Revision });
                    table.ForeignKey(
                        name: "FK_Question_TextAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "TextAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentOptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AssignmentQuestionID = table.Column<Guid>(nullable: true),
                    AssignmentQuestionRevision = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssignmentOptions_Question_AssignmentQuestionID_AssignmentQuestionRevision",
                        columns: x => new { x.AssignmentQuestionID, x.AssignmentQuestionRevision },
                        principalTable: "Question",
                        principalColumns: new[] { "ID", "Revision" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireQuestions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuestionID = table.Column<Guid>(nullable: true),
                    QuestionRevision = table.Column<int>(nullable: true),
                    QuestionnaireID = table.Column<Guid>(nullable: true),
                    QuestionnaireRevision = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_Question_QuestionID_QuestionRevision",
                        columns: x => new { x.QuestionID, x.QuestionRevision },
                        principalTable: "Question",
                        principalColumns: new[] { "ID", "Revision" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_Questionnaires_QuestionnaireID_QuestionnaireRevision",
                        columns: x => new { x.QuestionnaireID, x.QuestionnaireRevision },
                        principalTable: "Questionnaires",
                        principalColumns: new[] { "ID", "Revision" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SimpleAnswers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false),
                    SimpleQuestionID = table.Column<Guid>(nullable: true),
                    SimpleQuestionRevision = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SimpleAnswers_Question_SimpleQuestionID_SimpleQuestionRevision",
                        columns: x => new { x.SimpleQuestionID, x.SimpleQuestionRevision },
                        principalTable: "Question",
                        principalColumns: new[] { "ID", "Revision" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAnswers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AssignmentQuestionID = table.Column<Guid>(nullable: true),
                    AssignmentQuestionRevision = table.Column<int>(nullable: true),
                    CorrectOptionId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssignmentAnswers_AssignmentOptions_CorrectOptionId",
                        column: x => x.CorrectOptionId,
                        principalTable: "AssignmentOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentAnswers_Question_AssignmentQuestionID_AssignmentQuestionRevision",
                        columns: x => new { x.AssignmentQuestionID, x.AssignmentQuestionRevision },
                        principalTable: "Question",
                        principalColumns: new[] { "ID", "Revision" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnswers_CorrectOptionId",
                table: "AssignmentAnswers",
                column: "CorrectOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnswers_AssignmentQuestionID_AssignmentQuestionRevision",
                table: "AssignmentAnswers",
                columns: new[] { "AssignmentQuestionID", "AssignmentQuestionRevision" });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentOptions_AssignmentQuestionID_AssignmentQuestionRevision",
                table: "AssignmentOptions",
                columns: new[] { "AssignmentQuestionID", "AssignmentQuestionRevision" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_AnswerId",
                table: "Question",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireQuestions_QuestionID_QuestionRevision",
                table: "QuestionnaireQuestions",
                columns: new[] { "QuestionID", "QuestionRevision" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireQuestions_QuestionnaireID_QuestionnaireRevision",
                table: "QuestionnaireQuestions",
                columns: new[] { "QuestionnaireID", "QuestionnaireRevision" });

            migrationBuilder.CreateIndex(
                name: "IX_SimpleAnswers_SimpleQuestionID_SimpleQuestionRevision",
                table: "SimpleAnswers",
                columns: new[] { "SimpleQuestionID", "SimpleQuestionRevision" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnswers");

            migrationBuilder.DropTable(
                name: "QuestionnaireQuestions");

            migrationBuilder.DropTable(
                name: "SimpleAnswers");

            migrationBuilder.DropTable(
                name: "AssignmentOptions");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "TextAnswers");
        }
    }
}
