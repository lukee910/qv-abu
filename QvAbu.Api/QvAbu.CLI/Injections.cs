using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace QvAbu.CLI
{
    public static class Injections
    {
        public static ContainerBuilder AddCli(this ContainerBuilder builder)
        {
            builder.RegisterType<ImportExportService>().As<IImportExportService>();

            return builder;
        }
    }
}
