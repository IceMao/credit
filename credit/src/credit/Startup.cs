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
        public void ConfigureServices(IServiceCollection services)
        {
            var appEnv = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>();
            //services.AddEntityFramework()
            //    .AddSqlite()
            //    .AddDbContext<CreditContext>(x => x.UseSqlite("data source=" + appEnv.ApplicationBasePath + "/Database/credit.db"));

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<CreditContext>(x => x.UseSqlServer("server=localhost;uid=sa;password=Cream2015!@#;database=credit;"));//182.254.211.75

            services.AddIdentity<User, IdentityRole>(x => {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 0;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonLetterOrDigit = false;
                x.Password.RequireUppercase = false;
                x.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<CreditContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSmartUser<User, string>();
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
            app.UseMvc(x => x.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
            await SampleData.InitDB(app.ApplicationServices);
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
