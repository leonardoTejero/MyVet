using Infraestructure.Core.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyVet.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet
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
            #region Context  SQL Server
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("ConnectionStringSQLServer"));
            });
            #endregion

            //inyectar la semilla
            //services.AddTransient<SeedDb>(); 

            #region Inyeccion de dependencia
            DependencyInyectionHandler.DependencyInyectionConfig(services);
            #endregion

            //Autenticacion por Cookie
            #region Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //Tiempo que dura la autenticacion
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    //Si expira la aunticacion true/false
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                    //Si no tiene autorizacion de los controladores lo dirija al login
                    options.LoginPath = "/Auth/Login";
                });
            #endregion

            //Esto es para que refresque los datos en ejecución
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddControllersWithViews();
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

            //#1
            app.UseAuthentication();

            //#2
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
