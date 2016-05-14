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
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole<long>>>();

            if(db.Database != null && db.Database.EnsureCreated())
            {
                await roleManager.CreateAsync(new IdentityRole<long> { Name = "管理员" });
                await roleManager.CreateAsync(new IdentityRole<long> { Name = "联络员" });

                var user = new User {RealName="张三",PhoneNumber="18845296017",Email="123@qq.com", UserName = "admin",Level="99" };
                await userManager.CreateAsync(user, "Cream2015!@#");
                await userManager.AddToRoleAsync(user, "管理员");

                var manage = new User { RealName = "李四", PhoneNumber = "18845296017", Email = "123@qq.com", UserName = "Manage",Level="10" };
                await userManager.CreateAsync(manage, "Cream2015!@#");
                await userManager.AddToRoleAsync(manage, "管理员");

                var liaison = new User { UserName = "liaison",RegistrationNumber="123456789012345",EnterpriseName= "齐齐哈尔星图科技", RealName = "王二狗",LiaisonIdNumber="23272119901045874",LegalIdNumber= "232721196010423874",PhoneNumber="18874895210", Level = "1" };
                await userManager.CreateAsync(liaison, "Cream2015!@#");
                await userManager.AddToRoleAsync(liaison, "联络员");

                //初始化 公示类型
                db.TypeCS.Add(new TypeCS { NameType = "PType", Types = "正常" });
                db.TypeCS.Add(new TypeCS { NameType = "PType", Types = "经营异常" });
                db.TypeCS.Add(new TypeCS { NameType = "PType", Types = "严重违法" });
                //初始化 经营状态类型
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "开业" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "歇业" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "停业" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "清算" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "续存" });

                db.BaseInfo.Add(new BaseInfo { RegistrationNumber = "123456789012345", EnterpriseName = "齐齐哈尔星图科技" });
                db.BaseInfo.Add(new BaseInfo { RegistrationNumber = "123456711111111", EnterpriseName = "齐齐哈尔建华区华图教育" });
                db.BaseInfo.Add(new BaseInfo { RegistrationNumber = "123456722222222", EnterpriseName = "齐齐哈尔审计局" });
                db.InfoRandom.Add(new InfoRandom { EnterpriseName = "齐齐哈尔星图科技", RegistrationNumber = "123456789012345", DateTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                db.InfoRandom.Add(new InfoRandom { EnterpriseName = "齐齐哈尔建华区华图教育", RegistrationNumber = "123456711111111", DateTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                db.InfoRandom.Add(new InfoRandom { EnterpriseName = "齐齐哈尔审计局", RegistrationNumber = "123456722222222", DateTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                db.AnnouncementRandom.Add(new AnnouncementRandom { title = "抽查公告题目",Writer="张三", WriteTime = DateTime.Parse("2016/6/12"), Content = "这是抽查公告的内容", DateTime = DateTime.Parse("2016/3/22") });
                db.AnnouncementUnsual.Add(new AnnouncementUnsual { title = "异常题目", Writer = "张三", WriteTime = DateTime.Parse("2016/6/12"), Content = "这是异常公告的内容", DateTime = DateTime.Parse("2016/2/19") });
                db.AnnouncementIllegal.Add(new AnnouncementIllegal { title = "违法题目",Writer="李四",WriteTime=DateTime.Parse("2016/6/12"), Content = "这是违法公告的内容", DateTime = DateTime.Parse("2016/4/2") });
                db.InfoIllegal.Add(new InfoIllegal { RegistrationNumber = "123456789012345", EnterpriseName = "齐齐哈尔星图科技", DateTime=DateTime.Parse("2016/4/12") });
                db.InfoRandom.Add(new InfoRandom { RegistrationNumber = "123456711111111", EnterpriseName = "齐齐哈尔建华区华图教育", DateTime = DateTime.Parse("2016/2/12"), Result="正常" });
                db.InfoUnusual.Add(new InfoUnusual { RegistrationNumber = "123456722222222", EnterpriseName = "齐齐哈尔审计局", DateTime = DateTime.Parse("2016/3/10") });
            }
            db.SaveChanges();
        }
    }
}
