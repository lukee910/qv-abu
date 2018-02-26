using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QvAbu.Data.Data;
using QvAbu.Data;
using Autofac.Extensions.DependencyInjection;

namespace QvAbu.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfigurationRoot configuration)
        {
            this.Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddJsonFormatters()
                .AddFormatterMappings();

            var connection = this.Configuration.GetConnectionString();
            services.AddDbContext<QuestionsContext>(options =>
                options.UseSqlServer(connection)
            );

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

            // Autofac
            var containerBuilder = new ContainerBuilder();

            containerBuilder.AddApi()
                .AddData();

            containerBuilder.Populate(services);
            var applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory, 
                              QuestionsContext questionsContext)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseCors("AllowAll");

            app.UseMvc();

            DbInitializer.Initialize(env.IsDevelopment(), questionsContext);
        }
    }
}
