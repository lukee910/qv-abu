using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Data.Data
{
    public class DbInitializer
    {
        public static void Initialize(bool isDevelopment,
                                      QuestionsContext context)
        {
            context.Database.EnsureCreated();

            // Has been seeded?
            if (!isDevelopment || context.SimpleQuestions.Any())
            {
                return;
            }

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
            context.AssignmentOptions.AddRange(assignmentOptions);

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
            context.AssignmentAnswers.AddRange(assignmentAnswers);

            context.AssignmentQuestions.Add(new AssignmentQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Ordnen Sie die Lernorte a) - c) den Aussagen 1. - 4. zu.",
                Answers = assignmentAnswers,
                Options = assignmentOptions
            });

            // Multiple Choice
            var multipleChoiceId = Guid.NewGuid();
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
            context.SimpleAnswers.AddRange(simpleAnswers);
            context.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = multipleChoiceId,
                Revision = 1,
                Text = "Was trifft auf die Bundesverfassung (BV) zu?",
                Answers = simpleAnswers,
                SimpleQuestionType = SimpleQuestionType.SingleChoice
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
            context.SimpleAnswers.AddRange(simpleAnswers);
            context.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = multipleChoiceId,
                Revision = 2,
                Text = "Was trifft nicht auf die Bundesverfassung (BV) zu?",
                Answers = simpleAnswers,
                SimpleQuestionType = SimpleQuestionType.MultipleChoice
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
            context.SimpleAnswers.AddRange(simpleAnswers);
            context.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beurteilen Sie die Aussagen zu den Möglichkeiten nach der Lehre",
                Answers = simpleAnswers,
                SimpleQuestionType = SimpleQuestionType.TrueFalse
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
            context.SimpleAnswers.AddRange(simpleAnswers);
            context.SimpleQuestions.Add(new SimpleQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beurteilen Sie die Aussagen zu den Lernorten",
                Answers = simpleAnswers,
                SimpleQuestionType = SimpleQuestionType.MultipleChoice
            });

            // Text Questions
            var textAnswer = new TextAnswer
            {
                ID = Guid.NewGuid(),
                Text = "..."
            };
            context.TextAnswers.Add(textAnswer);
            context.TextQuestions.Add(new TextQuestion
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Text = "Beschreiben Sie die Hauptaufgabe der Berufsfachschule stichwortartig",
                Answer = textAnswer
            });

            context.SaveChanges();

            // -- Questionnaire /TODO: FIX THIS

            // Simple Questions
            var includedIds = new List<Guid>();
            var questions = new List<Question>();
            var dbQuestions = context.SimpleQuestions.ToList();
            foreach (var question in dbQuestions)
            {
                if (includedIds.Contains(question.ID))
                {
                    continue;
                }

                includedIds.Add(question.ID);
                questions.Add(dbQuestions
                    .Where(_ => _.ID == question.ID)
                    .OrderByDescending(_ => _.Revision)
                    .FirstOrDefault());
            }
            var questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 2,
                Name = "Multiple Choice"
            };
            questionnaire.QuestionnaireQuestions = questions
                .Select(_ => new QuestionnaireQuestion
                {
                    ID = Guid.NewGuid(),
                    Question = _,
                    Questionnaire = questionnaire
                })
                .ToList();
            context.Questionnaires.Add(questionnaire);

            questions = context.SimpleQuestions
                    .Where(_ => _.Revision == 1)
                    .ToList<Question>();
            questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = "Multiple Choice"
            };
            questionnaire.QuestionnaireQuestions = questions
                .Select(_ => new QuestionnaireQuestion
                {
                    ID = Guid.NewGuid(),
                    Question = _,
                    Questionnaire = questionnaire
                })
                .ToList();
            context.Questionnaires.Add(questionnaire);

            // All
            questions = new List<Question>();
            questions.AddRange(context.AssignmentQuestions);
            questions.AddRange(context.SimpleQuestions);
            questions.AddRange(context.TextQuestions);
            questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = "All questions"
            };
            questionnaire.QuestionnaireQuestions = questions
                .Select(_ => new QuestionnaireQuestion
                {
                    ID = Guid.NewGuid(),
                    Question = _,
                    Questionnaire = questionnaire
                })
                .ToList();
            context.Questionnaires.Add(questionnaire);

            // Save Changes
            context.SaveChanges();
        }
    }
}
