using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.Data.Data;
using QvAbu.Data.Data.Repository.Questions;
using QvAbu.Data.Data.UnitOfWork;

namespace QvAbu.Data
{
    public static class Injections
    {
        public static ContainerBuilder AddData(this ContainerBuilder builder)
        {
            return builder.AddDataContexts()
                .AddRepos()
                .AddUnitsOfWork();
        }

        private static ContainerBuilder AddDataContexts(this ContainerBuilder builder)
        {
            builder.RegisterType<QuestionsContext>()
                .As<IQuestionsContext>()
                .InstancePerLifetimeScope();

            return builder;
        }

        private static ContainerBuilder AddRepos(this ContainerBuilder builder)
        {
            builder.RegisterType<AssignmentQuestionsRepo>().As<IAssignmentQuestionsRepo>();
            builder.RegisterType<SimpleQuestionsRepo>().As<ISimpleQuestionsRepo>();
            builder.RegisterType<TextQuestionsRepo>().As<ITextQuestionsRepo>();
            builder.RegisterType<QuestionnairesRepo>().As<IQuestionnairesRepo>();

            return builder;
        }

        private static ContainerBuilder AddUnitsOfWork(this ContainerBuilder builder)
        {
            builder.RegisterType<QuestionsUnitOfWork>().As<IQuestionsUnitOfWork>();
            builder.RegisterType<QuestionnairesUnitOfWork>().As<IQuestionnairesUnitOfWork>();

            return builder;
        }
    }
}
