using Autofac;
using Microsoft.Extensions.DependencyInjection;
using QvAbu.CLI.Wrappers;

namespace QvAbu.CLI
{
    public static class Injections
    {
        public static ContainerBuilder AddCli(this ContainerBuilder builder)
        {
            builder.RegisterType<ImportExportService>().As<IImportExportService>();

            builder.RegisterType<FileWrapper>().As<IFile>();

            return builder;
        }
    }
}
