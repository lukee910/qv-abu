using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;
using QvAbu.Data.Data.Repository.Questions;
using QvAbu.Data.Data.UnitOfWork;

namespace QvAbu.Data
{
    public static class Injections
    {
        public static void AddAll(IServiceCollection services)
        {
            AddContexts(services);
            AddRepos(services);
            AddUnitsOfWork(services);
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
    }
}
