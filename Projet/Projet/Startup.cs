using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Projet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet
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
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHostedService<ResetPasswordCleanupService>();

            services.AddControllersWithViews();
            services.AddDbContext<AppDB>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            }
                );

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
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Login}/{action=Auth}");
                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Additional routes
                endpoints.MapControllerRoute(
                    name: "Inscription",
                    pattern: "{controller=Inscription}/{action=Inscription_User}");

                endpoints.MapControllerRoute(
                  name: "Checkemail",
                  pattern: "{controller=Resetpassword}/{action=Checkemail}");
                endpoints.MapControllerRoute(
name: "sendemail",
pattern: "{controller=Resetpassword}/{action=sendemail}");

                // Add more routes as needed
            

               endpoints.MapControllerRoute(
    name: "CreateUser",
    pattern: "{controller=Inscription}/{action=Create}");
                endpoints.MapControllerRoute(
  name: "Auth",
  pattern: "{controller=Login}/{action=authentification}");
                endpoints.MapControllerRoute(
            name: "NewPassword",
            pattern: "NewPassword/{key}",
            defaults: new { controller = "ResetPassword", action = "NewPassword" }
        );
                endpoints.MapControllerRoute(
name: "setnewpassword",
pattern: "{controller=Resetpassword}/{action=setNewPassword}");
                endpoints.MapControllerRoute(
name: "Logout",
pattern: "{controller=Home}/{action=Logout}");
                endpoints.MapControllerRoute(
name: "Setting",
pattern: "{controller=Home}/{action=Setting}");
                endpoints.MapControllerRoute(
name: "UpdateUser",
pattern: "{controller=Home}/{action=UpdateUser}");
                endpoints.MapControllerRoute(
name: "Prediction",
pattern: "{controller=Predict}/{action=Prediction}");
                endpoints.MapControllerRoute(
name: "SubmitForm",
pattern: "{controller=Predict}/{action=SubmitForm}");
                
                // Add more routes as needed
            });
          

            
    }
    }
}
