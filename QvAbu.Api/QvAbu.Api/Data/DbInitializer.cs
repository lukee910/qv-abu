using System;
using System.Collections.Generic;
using System.Linq;
using QvAbu.Api.Models.Questions;
using Microsoft.AspNetCore.Hosting;
using QvAbu.Api.Models.Questionnaire;

namespace QvAbu.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(IHostingEnvironment env, 
                                      QuestionsContext questionContext)
        {
            questionContext.Database.EnsureCreated();

            // Has been seeded?
            if (!env.IsDevelopment() || questionContext.SimpleQuestions.Any())
            {
                return;
            }

            // TODO: Add seeding data
            // see https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro#add-code-to-initialize-the-database-with-test-data

            // Assignment Questions
            var assignmentOptions = new List<AssignmentOption>
            {
                new AssignmentOption
                {
                    ID = Guid.NewGuid(),
                    Text = "ÜK"
                },
                new AssignmentOption
                {
                    ID = Guid.NewGuid(),
                    Text = "Berufsfachschule"
                },
                new AssignmentOption
                {
                    ID = Guid.NewGuid(),
                    Text = "Lehrbetrieb"
                }
            };
            questionContext.AssignmentOptions.AddRange(assignmentOptions);

            var assignmentAnswers = new List<AssignmentAnswer>
            {
                new AssignmentAnswer
                {
                    ID = Guid.NewGuid(),
                    CorrectOption = assignmentOptions[2],
                    Text = "Dieser Lernort verpflichtet sich, Sie fachgemäss auszubilden."
                },
                new AssignmentAnswer
                {
                    ID = Guid.NewGuid(),
                    CorrectOption = assignmentOptions[0],
                    Text = "Hier erlernen Sie grundlegende praktische und theoretische " +
                           "Fähigkiten Ihres Berufs."
                },
                new AssignmentAnswer
                {
                    ID = Guid.NewGuid(),
                    CorrectOption = assignmentOptions[1],
                    Text = "Sie erlernen theoretische Hintergründe und bereiten sich au das Berufs- " +
                           "und Privatleben vor."
                },
                new AssignmentAnswer
                {
                    ID = Guid.NewGuid(),
                    CorrectOption = assignmentOptions[2],
                    Text = "An diesem Lernort verbringen Sie die meiste Zeit Ihrer Ausbildung."
                }
            };
            questionContext.AssignmentAnswers.AddRange(assignmentAnswers);

            questionContext.AssignmentQuestions.Add(new AssignmentQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Ordnen Sie die Lernorte a) - c) den Aussagen 1. - 4. zu.",
                Answers = assignmentAnswers,
                Options = assignmentOptions
            });

            // Multiple Choice
            var multipleChoiceID = Guid.NewGuid();
            var simpleAnswers = new List<SimpleAnswer>
            {
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Die BV ist das Grundgesetz der Schweiz"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Das Personenrecht ist in der BV enthalten"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Die BV gilt nur auf Bundesebene"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Die BV richtet sich nicht nach den Menschenrechten"
                }
            };
            questionContext.SimpleAnswers.AddRange(simpleAnswers);
            questionContext.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = multipleChoiceID,
                Revision = 1,
                Text = "Was trifft auf die Bundesverfassung (BV) zu?",
                Answers = simpleAnswers,
                IsMultipleChoice = false,
                IsNumberOfAnswersGiven = false
            });
            // Multiple Choice, Revision 2
            simpleAnswers = new List<SimpleAnswer>
            {
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Die BV ist das Grundgesetz der Schweiz"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Das Personenrecht ist in der BV enthalten"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Die BV gilt nur auf Bundesebene"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Die BV richtet sich nicht nach den Menschenrechten"
                }
            };
            questionContext.SimpleAnswers.AddRange(simpleAnswers);
            questionContext.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = multipleChoiceID,
                Revision = 2,
                Text = "Was trifft nicht auf die Bundesverfassung (BV) zu?",
                Answers = simpleAnswers,
                IsMultipleChoice = true,
                IsNumberOfAnswersGiven = false
            });

            // Pick a certain number
            simpleAnswers = new List<SimpleAnswer>
            {
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Sie können die Berufsmatura (BM) berufsbegleitend absolvieren"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Sie können direkt an eine Fachhochschule gehen"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Um ein Studium an einer Universität zu beginnen, genügt die erfolgreich " +
                           "absolvierte BM"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Sie können die BM nur als Vollzeitausbildung machen"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Mit dem EFZ können Sie an eine Höhere Fachschule gehen"
                }
            };
            questionContext.SimpleAnswers.AddRange(simpleAnswers);
            questionContext.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beurteilen Sie die Aussagen zu den Möglichkeiten nach der Lehre",
                Answers = simpleAnswers,
                IsMultipleChoice = true,
                IsNumberOfAnswersGiven = true
            });

            // True/False
            simpleAnswers = new List<SimpleAnswer>
            {
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "In der Berufsfachschule erhalten Sie nur theoretisches Grundwissen " +
                           "für ihren Beruf"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = false,
                    Text = "Überbetriebliche Kurse vertiefen nur theoretische Grundlagen"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Im Lehrbetrieb erwerben Sie Ihr berufsliches Wissen und Können"
                },
                new SimpleAnswer
                {
                    ID = Guid.NewGuid(),
                    IsCorrect = true,
                    Text = "Die Berufsfachschule vermittelt theoretische Kenntnisse für den Beruf " +
                           "und bereitet Sie auch auf Ihr privates Leben vor"
                }
            };
            questionContext.SimpleAnswers.AddRange(simpleAnswers);
            questionContext.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beurteilen Sie die Aussagen zu den Lernorten",
                Answers = simpleAnswers,
                IsMultipleChoice = true,
                IsNumberOfAnswersGiven = false
            });

            // Text Questions
            var textAnswer = new TextAnswer
            {
                ID = Guid.NewGuid(),
                Text = "..."
            };
            questionContext.TextAnswers.Add(textAnswer);
            questionContext.TextQuestions.Add(new TextQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beschreiben Sie die Hauptaufgabe der Berufsfachschule stichwortartig",
                Answer = textAnswer
            });

            // Save Changes
            questionContext.SaveChanges();

            // -- Questionnaire /TODO: FIX THIS

            // Simple Questions
            questionContext.Questionnaires.Add(new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = "Multiple Choice, newest",
                Questions = new List<Question>(questionContext.SimpleQuestions
                    .Where(_ => _.ID != multipleChoiceID || _.Revision == 2))
            });
            questionContext.Questionnaires.Add(new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = "Multiple Choice, Revision 1",
                Questions = new List<Question>(questionContext.SimpleQuestions
                    .Where(_ => _.Revision == 1))
            });

            // All
            var questions = new List<Question>();
            questions.AddRange(questionContext.AssignmentQuestions);
            questions.AddRange(questionContext.SimpleQuestions);
            questions.AddRange(questionContext.TextQuestions);
            questionContext.Questionnaires.Add(new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = "Multiple Choice, Revision 1",
                Questions = questions
            });

            // Save Changes
            questionContext.SaveChanges();
        }
    }
}
