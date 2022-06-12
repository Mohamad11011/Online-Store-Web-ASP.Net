using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using MySql.Data.EntityFramework;
using Owin;
using System;
using System.Data.Entity;
using Testing1.Models;

[assembly: OwinStartupAttribute(typeof(Testing1.Startup))]
namespace Testing1
{
    public partial class Startup
    {
        public Startup() 
        {
            
        }

        public void Configuration(IAppBuilder app)
        { 
            ConfigureAuth(app);
            

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\Users\user\Desktop\Test2\Testing1\Testing1\Testing1\App_Data\ShopData.mdf;Integrated Security=True;Encrypt=False;TrustServerCertificate=False")
                 );

            services.AddSingleton<IHostingEnvironment>();
            services.AddScoped<ShopContext>();


        }
    }
}
