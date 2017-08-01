using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using QvAbu.Data;
using QvAbu.Data.Data;

namespace QvAbu.CLI
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json", false, true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                //.AddEnvironmentVariables()
                ;

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = this.Configuration["QvAbuConnection"] ?? "Data Source=.;Initial Catalog=QvAbu;Integrated Security=True;";
            services.AddDbContext<QuestionsContext>(options =>
                options.UseSqlServer(connection)
            );

            Injections.AddAll(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(QuestionsContext questionsContext)
        {
            DbInitializer.Initialize(false, questionsContext);
        }
    }
}
