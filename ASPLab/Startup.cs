using ASPLab.Data.DB;
using ASPLab.Data.DB.Context;
using ASPLab.Data.DB.Repository;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Mocks;
using ASPLab.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPLab
{
    public class Startup
    {
        private IConfigurationRoot _confRoot;
        public Startup(IHostEnvironment hostEnvironment)
        {
            _confRoot = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile(@"Data/DB/dbsettings.json")
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAllDish, DishRepository>();
            services.AddTransient<IDishCategory, CategoryRepository>();
            services.AddTransient<IUser, UserRepository>();
            services.AddTransient<IOrder, OrderRepository>();
            services.AddMvc(mvcOptions => { mvcOptions.EnableEndpointRouting = false; });
            services.AddDbContext<AppDBContent>(options =>
                options.UseSqlServer(_confRoot.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();  
            app.UseStatusCodePages();        
            app.UseStaticFiles();             
            app.UseMvcWithDefaultRoute();     
            using(var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent appDBContent = scope.ServiceProvider.GetService<AppDBContent>();
                appDBContent.Database.EnsureCreated();
                DBObjectsInit.DefaultDBObjectsInitialization(appDBContent);
            }
            
        }
    }
}
