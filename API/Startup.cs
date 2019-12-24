using Infrasctruture.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using MyAPI.Configuration;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using MyAPI.Extensions;

namespace MyAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(hostEnvironment.ContentRootPath)
              .AddJsonFile("appsettings.json", true, true)
              .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<APIContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });
            services.AddIdentityConfiguration(Configuration);
            services.ResolveDependencies();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
