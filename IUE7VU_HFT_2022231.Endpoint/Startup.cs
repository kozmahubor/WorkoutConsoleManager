using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IUE7VU_HFT_2022231.Models;
using IUE7VU_HFT_2022231.Repository;
using IUE7VU_HFT_2022231.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

namespace IUE7VU_HFT_2022231.Endpoint
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
            services.AddTransient<IUE7VUDbContext>();

            services.AddTransient<IRepository<Person>, PersonRepository>();
            services.AddTransient<IRepository<Workout>, WorkoutRepository>();
            services.AddTransient<IRepository<Membership>, MembershipRepository>();
            services.AddTransient<IRepository<Trainer>, TrainerRepository>();

            services.AddTransient<IPersonLogic, PersonLogic>();
            services.AddTransient<IWorkoutLogic, WorkoutLogic>();
            services.AddTransient<IMembershipLogic, MembershipLogic>();
            services.AddTransient<ITrainerLogic, TrainerLogic>();

            //services.AddRazorPages();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IUE7VU_HFT_2022231.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    //c.RoutePrefix = "api";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IUE7VU_HFT_2022231.Endpoint v1");
                });
            }
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
