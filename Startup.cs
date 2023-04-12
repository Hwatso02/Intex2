using Intex2.Data;
using Intex2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
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

            ////Temporary middleware for testing, don't use in production!
            //app.Use(async (context, next) =>
            //{
            //    var userIdentity = new ClaimsIdentity(new[] {
            //        new Claim(ClaimTypes.Role, "Admin")
            //    }, "Demo");
            //    context.User = new ClaimsPrincipal(userIdentity);
            //    await next.Invoke();
            //});

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "img-src 'self'; script-src 'self'");
                await next();
            });

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
