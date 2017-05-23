using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QvAbu.Api.Data;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Data.UnitOfWork;
using QvAbu.Api.Data.Repository.Questions;
using QvAbu.Api.Services.Questions;
using Newtonsoft.Json;

namespace QvAbu.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            //if (env.IsDevelopment())
            //{
            //    builder.AddUserSecrets("9BE1513D90384D9EAE8655AF1DAD67CA");
            //}

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddJsonFormatters()
                .AddFormatterMappings();

            var connection = this.Configuration["QvAbuConnection"];
            services.AddDbContext<QuestionsContext>(options =>
                options.UseSqlServer(connection)
            );

            AddInjections(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory, 
                              QuestionsContext questionsContext)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            DbInitializer.Initialize(env, questionsContext);
        }

        private static void AddInjections(IServiceCollection services)
        {
            #region Questions

            // Contexts
            services.AddScoped<QuestionsContext>();

            // Repos
            services.AddScoped<IAssignmentQuestionsRepo, AssignmentQuestionsRepo>();
            services.AddScoped<ISimpleQuestionsRepo, SimpleQuestionsRepo>();
            services.AddScoped<ITextQuestionsRepo, TextQuestionsRepo>();

            // UnitOfWork
            services.AddScoped<IQuestionsUnitOfWork, QuestionsUnitOfWork>();

            // Services
            services.AddScoped<IQuestionsService, QuestionsService>();

            #endregion

            #region Questionnaire

            // Repos
            services.AddScoped<IQuestionnaireRepo, QuestionnaireRepo>();

            // UnitOfWork
            services.AddScoped<IQuestionnaireUnitOfWork, QuestionnaireUnitOfWork>();

            // Services
            services.AddScoped<IQuestionnaireService, QuestionnaireService>();

            #endregion
        }
    }
}
