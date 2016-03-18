using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using credit.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Logging;

namespace credit
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var appEnv = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<CreditContext>(x => x.UseSqlServer("server=localhost;uid=sa;password=123456;database=credit;"));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CreditContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, ILoggerFactory logger)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            logger.MinimumLevel = LogLevel.Information;
            logger.AddConsole();
            logger.AddDebug();
            app.UseIdentity();
            app.UseMvc(x => x.MapRoute("default", "{controller=Account}/{action=Login}/{id?}"));
            await SampleData.InitDB(app.ApplicationServices);
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
