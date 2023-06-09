using Intex2.Data;
using Intex2.Models;
using Microsoft.AspNetCore.Rewrite;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Intex2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
    
            // Set the PYTHONNET_PYDLL environment variable
            var pythonPath = @"C:\path\to\your\python\installation"; // Replace with your actual Python installation path
            var pythonDllPath = Path.Combine(pythonPath, "pythonXX.dll"); // Replace XX with the Python version you installed, e.g., python38.dll for Python 3.8
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath);
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });





            string defaultConnectionString = string.Format(
              Configuration.GetConnectionString("DefaultConnection"),
              Environment.GetEnvironmentVariable("DEFAULT_DB_HOST"),
              Environment.GetEnvironmentVariable("DEFAULT_DB_PORT"),
              Environment.GetEnvironmentVariable("DEFAULT_DB_NAME"),
              Environment.GetEnvironmentVariable("DEFAULT_DB_USER"),
              Environment.GetEnvironmentVariable("DEFAULT_DB_PASSWORD")
          );

            string authLinkConnectionString = string.Format(
                Configuration.GetConnectionString("AuthLink"),
                Environment.GetEnvironmentVariable("AUTH_DB_HOST"),
                Environment.GetEnvironmentVariable("AUTH_DB_PORT"),
                Environment.GetEnvironmentVariable("AUTH_DB_NAME"),
                Environment.GetEnvironmentVariable("AUTH_DB_USER"),
                Environment.GetEnvironmentVariable("AUTH_DB_PASSWORD")
            );

            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(authLinkConnectionString));

            services.AddDbContext<egyptContext>(options =>
                options.UseNpgsql(defaultConnectionString));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 20;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();



            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 20;
                options.Password.RequiredUniqueChars = 0;
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; img-src 'self' https://cwadmin.byu.edu ; script-src 'self'; style-src 'self' https://getbootstrap.com/; connect-src 'self' https://fag-el-gamous--group-1-12.is404.net:8000;");
                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
         

            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
