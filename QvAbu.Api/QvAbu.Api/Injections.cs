using Microsoft.Extensions.DependencyInjection;
using QvAbu.Api.Data;
using QvAbu.Api.Data.Repository;
using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Services.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api
{
    public static class Injections
    {
        public static void AddAll(IServiceCollection services)
        {
            AddContexts(services);
            AddRepos(services);
            AddUnitsOfWork(services);
            AddServices(services);
        }

        public static void AddContexts(IServiceCollection services)
        {
            services.AddScoped<IQuestionsContext, QuestionsContext>();
        }

        public static void AddRepos(IServiceCollection services)
        {
            services.AddScoped<IAssignmentQuestionsRepo, AssignmentQuestionsRepo>();
            services.AddScoped<ISimpleQuestionsRepo, SimpleQuestionsRepo>();
            services.AddScoped<ITextQuestionsRepo, TextQuestionsRepo>();
            services.AddScoped<IQuestionnairesRepo, QuestionnairesRepo>();
        }

        public static void AddUnitsOfWork(IServiceCollection services)
        {
            services.AddScoped<IQuestionsUnitOfWork, QuestionsUnitOfWork>();
            services.AddScoped<IQuestionnairesUnitOfWork, QuestionnairesUnitOfWork>();

        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionsService, QuestionsService>();
        }
    }
}
