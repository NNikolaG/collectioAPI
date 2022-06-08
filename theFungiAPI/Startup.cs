using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theFungiAPI.Core;
using theFungiApplication;
using theFungiApplication.Commands;
using theFungiApplication.Queries;
using theFungiDataAccess;
using theFungiImplementation.Commands;
using theFungiImplementation.Loggers;
using theFungiImplementation.Queries;
using theFungiImplementation.Validators;

namespace theFungiAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<theFungiDbContext>();
            services.AddTransient<UseCaseExecutor>();
            services.AddTransient<IApplicationActor, FakeActor>(); 
            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();


            //Queries
            services.AddTransient<IGetCategoriesQuery, EFGetCollections>();
            services.AddTransient<IGetSingleCollectionQuery, EFGetSingleCollectionQuery>();
            services.AddTransient<IGetSingleCollectionItemQuery, EFGetSingleCollectionItemQuery>();


            //Commands
            services.AddTransient<ICreateCollectionCommand, EFCreateCollectionCommand>();
            services.AddTransient<ICreateCollectionItemCommand, EFCreateCollectionItemCommand>();
            services.AddTransient<ICreateCollectionItemInfoCommand, EFCreateCollectionItemInfoCommand>();
            services.AddTransient<ICreateFollowCommand, EFCreateFollowCommand>();

            //Validators
            services.AddTransient<CreateCollectionValidator>();
            services.AddTransient<CreateCollectionItemValidator>();
            services.AddTransient<CreateCollectionItemInfoValidator>();
            services.AddTransient<CreateFollowValidator>();

            //Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<UseGlobalExceptionHandler>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
