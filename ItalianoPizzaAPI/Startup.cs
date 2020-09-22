using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItalianoPizzaAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ItalianoPizzaAPI
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
            services.AddDbContext<PizzaContext>(options => options.UseInMemoryDatabase(databaseName: "ItalianoPizzaDB"));
            services.AddControllers();
           
           services.AddScoped<IPizzaRepository, PizzaRepository>();

            var mapperConfig = new MapperConfiguration(mc => {
               mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Italiano Pizza";
                    document.Info.Description = "Italiano Pizzeria API to add new Pizzas";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Karthik Iyer",
                        Email = string.Empty,
                        Url = "https://www.linkedin.com/in/karthikiyer1304"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Open Source",
                        Url = string.Empty
                    };
                };
            }); 

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
