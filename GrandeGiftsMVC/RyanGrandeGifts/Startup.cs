using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using RyanGrandeGifts.Services;
using Microsoft.Extensions.Configuration;
using RyanGrandeGifts.Models;

namespace RyanGrandeGifts
{
    public class Startup
    {
        // properties
        public IConfiguration Configuration { get; }

        // constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddDistributedMemoryCache();
            services.AddDbContext<MyDbContext>( options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddSession();
            services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {
                    config.User.RequireUniqueEmail = false;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireDigit = true;
                    config.Password.RequireNonAlphanumeric = false;
                }
            ).AddEntityFrameworkStores<MyDbContext>();
            //  services.AddScoped<IDataService<>, DataService<>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Product>, DataService<Product>>();
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<HamperOrder>, DataService<HamperOrder>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvcWithDefaultRoute();

            //SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
