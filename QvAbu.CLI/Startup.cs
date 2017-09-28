using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connection = this.Configuration.GetConnectionString();
            services.AddDbContext<QuestionsContext>(options =>
                options.UseSqlServer(connection)
            );

            // Autofac
            var containerBuilder = new ContainerBuilder();

            containerBuilder.AddCli()
                .AddData();

            containerBuilder.Populate(services);
            this.ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(QuestionsContext questionsContext)
        {
            DbInitializer.Initialize(false, questionsContext);
        }
    }
}
