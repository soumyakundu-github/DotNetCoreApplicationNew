using DotNetCoreApplication.Models;
using DotNetCoreApplication.Models.Interface;
using DotNetCoreApplication.Models.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication
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
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DotNetCoreDbAccess")));  //Connection String Establish here

            //Inject Dependency injection Declare Here
            services.AddTransient<ILocation, LocationRepository>(); //We used AddTransient Dependency


            //services.AddSingleton<IStudentRepository, TestStudentRepository>();  //AddSingleton type dependency
            //services.AddTransient<IStudentRepository, TestStudentRepository>();  //AddTransient type dependency
            //services.AddScoped<IStudentRepository, TestStudentRepository>();  //AddScoped type dependency
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=LocationDependencyInjection}/{action=Index}/{id?}");
            });
        }
    }
}
