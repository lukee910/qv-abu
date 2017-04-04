using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace QvAbu.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuestionsContext context)
        {
            context.Database.EnsureCreated();

            // Has been seeded?
            //if (context.SimpleQuestions.Any())
            //{
            //    return;
            //}

            // TODO: Add seeding data
            // see https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro#add-code-to-initialize-the-database-with-test-data
        }
    }
}
