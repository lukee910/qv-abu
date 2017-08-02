using Microsoft.Extensions.DependencyInjection;

namespace QvAbu.CLI
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
            services.AddScoped<IImportExportService, ImportExportService>();
        }
    }
}
