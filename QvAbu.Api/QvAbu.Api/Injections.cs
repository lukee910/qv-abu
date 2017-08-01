using Microsoft.Extensions.DependencyInjection;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api
{
    public static class Injections
    {
        public static void AddAll(IServiceCollection services)
        {
            Data.Injections.AddAll(services);
            AddServices(services);
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionsService, QuestionsService>();
        }
    }
}
