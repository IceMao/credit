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

                var user = new User {RealName="张晨飞",PhoneNumber="18845296017",Email="1234@163.com", UserName = "admin",Level="99" };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, "管理员");

                var manage = new User { RealName = "李顾德", PhoneNumber = "18845296017", Email = "123@qq.com", UserName = "Manage",Level="10" };
                await userManager.CreateAsync(manage, "Cream2015!@#");
                await userManager.AddToRoleAsync(manage, "管理员");

                var liaison = new User { RealName = "王志强", UserName = "liaison",RegistrationNumber="123456789012345",EnterpriseName= "齐齐哈尔星图科技", LiaisonIdNumber="23272119901045874",LegalIdNumber= "232721196010423874",PhoneNumber="18874895210", Level = "1" };
                await userManager.CreateAsync(liaison, "Cream2015!@#");
                await userManager.AddToRoleAsync(liaison, "联络员");

                //基础信息
                db.TypeCS.Add(new TypeCS { NameType = "basein", descript = "基础信息", Types = "无" });
                //初始化 抽查，异常，违法公告，公告类型名称
                db.TypeCS.Add(new TypeCS { NameType = "R",descript="这是抽查公告分类",Types = "无" });
                db.TypeCS.Add(new TypeCS { NameType = "U", descript = "这是经营异常公告分类", Types = "无" });
                db.TypeCS.Add(new TypeCS { NameType = "I", descript = "这是这是违法公告分类", Types = "无" });
                //初始化 抽查，异常，违法公示，公示类型名称
                db.TypeCS.Add(new TypeCS { NameType = "Rin",descript= "这是抽查公示分类", Types = "无" });
                db.TypeCS.Add(new TypeCS { NameType = "Uin", descript = "这是经营异常公示分类", Types = "无" });
                db.TypeCS.Add(new TypeCS { NameType = "Iin", descript = "这是这是违法公示分类", Types = "无" });
                //初始化 公示类型
                db.TypeCS.Add(new TypeCS { NameType = "RType", Types = "正常" ,descript="这是公示结果类型"});
                db.TypeCS.Add(new TypeCS { NameType = "RType", Types = "经营异常", descript = "这是公示结果类型" });
                db.TypeCS.Add(new TypeCS { NameType = "RType", Types = "严重违法", descript = "这是公示结果类型" });
                //初始化 经营状态类型
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "开业", descript = "这是经营状态类型" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "歇业", descript = "这是经营状态类型" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "停业", descript = "这是经营状态类型" });
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "清算" , descript = "这是经营状态类型"});
                db.TypeCS.Add(new TypeCS { NameType = "EType", Types = "续存", descript = "这是经营状态类型" });

                //db.BaseInfo.Add(new BaseInfo { RegisteNumber = "123456789012345", CompanyName = "齐齐哈尔星图科技" });
                //db.BaseInfo.Add(new BaseInfo { RegisteNumber = "123456711111111", CompanyName = "齐齐哈尔建华区华图教育" });
                //db.BaseInfo.Add(new BaseInfo { RegisteNumber = "123456722222222", CompanyName = "齐齐哈尔审计局" });
                //db.Info.Add(new Info { EnterpriseName = "齐齐哈尔星图科技", RegistrationNumber = "123456789012345", PublicTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                //db.Info.Add(new Info { EnterpriseName = "齐齐哈尔建华区华图教育", RegistrationNumber = "123456711111111", PublicTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                //db.Info.Add(new Info { EnterpriseName = "齐齐哈尔审计局", RegistrationNumber = "123456722222222", PublicTime = DateTime.Parse("2016/1/12"), Result = "正常" });
                //db.Announcement.Add(new Announcement { Title = "即时信息公示情况抽查", Writer = "System", PublicUnit = "黑垦字【2015】43号", WriteTime = DateTime.Parse("2016/6/12"), Content = "这是抽查公告的内容", PublicTime = DateTime.Parse("2016/3/22") });
                ////db.AnnouncementUnsual.Add(new AnnouncementUnsual { title = "责令限期履行公示义务通知书", Writer = "System", publicUnit = "黑垦字【2015】4号", WriteTime = DateTime.Parse("2016/6/12"), Content = "经查，你单位未依法履行 即时 信息公示义务。根据《企业信息公示暂行条例》第十条、《经营异常名录管理办法》第七条的规定，限你单位在10日内履行公示义务。逾期不履行的，将依法被列入经营异常名录。", DateTime = DateTime.Parse("2016/2/19") });
                //db.AnnouncementIllegal.Add(new AnnouncementIllegal { title = "违法题目", Writer = "System", publicUnit = "黑垦字【2015】20号", WriteTime=DateTime.Parse("2016/6/12"), Content = "这是违法公告的内容", DateTime = DateTime.Parse("2016/4/2") });

                //db.InfoIllegal.Add(new InfoIllegal { RegistrationNumber = "123456789012345", EnterpriseName = "齐齐哈尔星图科技", DateTime=DateTime.Parse("2016/4/12") });                
                //db.InfoUnusual.Add(new InfoUnusual { RegistrationNumber = "123456722222222", EnterpriseName = "齐齐哈尔审计局", DateTime = DateTime.Parse("2016/3/10") });

            }
            db.SaveChanges();
        }
    }
}
