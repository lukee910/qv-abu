using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Api.Services.Questions;

namespace QvAbu.Api
{
    public static class Injections
    {
        public static ContainerBuilder AddApi(this ContainerBuilder builder)
        {
            return builder.AddServices();
        }

        public static ContainerBuilder AddServices(this ContainerBuilder builder)
        {
            builder.RegisterType<QuestionsService>().As<IQuestionsService>();

            return builder;
        }
    }
}
