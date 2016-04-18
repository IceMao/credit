using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetRequiredService<CreditContext>();

            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            if(db.Database != null && db.Database.EnsureCreated())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "管理员" });
                await roleManager.CreateAsync(new IdentityRole { Name = "联络员" });

                var user = new User { UserName = "admin" };
                await userManager.CreateAsync(user, "Cream2015!@#");
                await userManager.AddToRoleAsync(user, "管理员");

                var liaison = new User { UserName = "liaison" };
                await userManager.CreateAsync(liaison, "Cream2015!@#");//此处不用密码登录？？
                await userManager.AddToRoleAsync(liaison, "联络员");

                db.BaseInfo.Add(new BaseInfo { RegistrationNumber = "123456789012345", EnterpriseName = "哈尔滨码锋科技" });
                db.InfoRandom.Add(new InfoRandom { EnterpriseName = "哈尔滨码锋科技", RegistrationNumber = "123456789012345", DateTime = DateTime.Now, Result = "正常" });
                db.AnnouncementRandom.Add(new AnnouncementRandom { title = "抽查公告题目", Content = "这是抽查公告的内容", DateTime = DateTime.Now });
            }
            db.SaveChanges();
        }
    }
}
