using CustomerAPI.CommandService;
using CustomerAPI.Database.Context;
using CustomerAPI.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICustomerDetailCommandService, CustomerDetailCommandService>();

            //add configuration settings wrapper mapping.
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //DbContext Dependency Injection
            services.AddDbContext<CustomerDetailContext>(options =>

                options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            // File should be ENVironment Specific
            IConfigurationBuilder builder = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json") // This file will be overridden by below next line 
                                    .AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: true); // Read ENV value for appsetting

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
            .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

          
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
