using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using credit.Models;
using Microsoft.Data.Entity;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    [Authorize (Roles ="管理员")]
    public class AdminController : BaseController
    {
        #region 联络员管理
        [HttpGet]
        public IActionResult DetailsLiaison()
        {
            var liaison = DB.Users
                .Where(x => x.Level == "1")
                .OrderBy(x=>x.RegisterTime)
                .ToList();
            ViewBag.LiasionCount = liaison.Count;
            return PagedView(liaison,8);
        }
        #endregion

        #region 显示，删除，创建管理员    
        public IActionResult DeleteUser(int id)
        {
            //var UserCurrent = DB.Users
            //        .Where(x => x.UserName == HttpContext.User.Identity.Name)
            //        .SingleOrDefault();
            if(User.Current.Level == "99")
            {
                var user = DB.Users
                .Where(x => x.Id == id)
                .SingleOrDefault();
                if (user == null)
                {
                    return Content("error");
                }
                else
                {
                    DB.Users.Remove(user);
                    DB.SaveChanges();
                    return Content("success");
                }
            }
            else
            {
                return Content("notRemove");
            }
            
        }
        [HttpGet]
        public IActionResult DetailsUser()
        {
            var user = DB.Users
                .Where(x => x.Level == "99" || x.Level == "10")
                .OrderByDescending(x=>x.Level)
                .OrderBy(x => x.RegisterTime)
                .ToList();
            ViewBag.UserNumber = user.Count();
            return PagedView(user,7);
        }
        //创建管理员
        [HttpGet]
        public IActionResult CreateManage()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
                if (UserCurrent.Level == "99")
                {
                    return View();
                }
                else
                {
                    return Content("不是超级用户，不可以添加管理员");
                }
            }
            else
            {
                return Content("请登录");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateManage(string username, string password, string RealName, string Email, string PhoneNumber)
        {
            var user = DB.Users
                .Where(x => x.UserName == username)
                .SingleOrDefault();
            if (user == null)
            {
                var manager = new User
                {
                    UserName = username,
                    Level = "10",
                    RegisterTime = DateTime.Now,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    RealName = RealName,
                };
                await UserManager.CreateAsync(manager, password);
                await UserManager.AddToRoleAsync(manager, "管理员");
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("usernameHave");
            }
        }
        #endregion
        #region  注册号基本表（增删改查）
        [HttpGet]
        public IActionResult DetailsBaseInfo()
        {
            return PagedView(DB.BaseInfo, 8);
        }
        
        [HttpGet]
        public IActionResult CreateBaseInfo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBaseInfo(BaseInfo baseInfo)
        {
            var oldBase = DB.BaseInfo
                .Include(x=>x.TypeCS)
                .Where(x => x.RegisteNumber == baseInfo.RegisteNumber)
                .SingleOrDefault();
            if (oldBase != null)
            {
                return Content("error");
            }
            else
            {
                baseInfo.TypeId = DB.TypeCS.Where(x => x.NameType == "basein").SingleOrDefault().Id;
                DB.BaseInfo.Add(baseInfo);
                DB.SaveChanges();
                return Content("success");
            }
            
        }
        [HttpGet]
        public IActionResult EditBaseInfo(int id)
        {
            var baseInfo = DB.BaseInfo
                .Include(x=>x.TypeCS)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (baseInfo == null)
                return Content("查无此人");
            else
                return View(baseInfo);
        }
        
        [HttpPost]
        public IActionResult EditBaseInfo(int id, BaseInfo BaseInfo)
        {
            var baseInfo = DB.BaseInfo
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            baseInfo.CompanyName = BaseInfo.CompanyName;
            baseInfo.RegisteNumber = BaseInfo.RegisteNumber;
            baseInfo.TypeCS.NameType = "basein";
            DB.SaveChanges();
            return Content("success");

        }
        //删除基本信息
        public IActionResult DeleteBaseInfo(int id)
        {
            var baseInfo = DB.BaseInfo
                .Where(x => x.Id == id)
                .SingleOrDefault();
            DB.BaseInfo.Remove(baseInfo);
            DB.SaveChanges();

            return Content("success");
        }
        #endregion

        #region 抽查公告
        //抽查公告
        public IActionResult DetailsAnnouncementRandom()
        {
            var ARandom = DB.Announcement 
                .Include(x => x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "R")
                .OrderByDescending(x=>x.WriteTime)
                .ToList();
            return PagedView(ARandom, 10);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementRandom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementRandom(Announcement ARandom)
        {

            DB.Announcement.Add(ARandom);
            var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
            ARandom.TypeId = DB.TypeCS.Where(x=>x.NameType == "R").SingleOrDefault().Id;
            ARandom.WriteTime = DateTime.Now;
            ARandom.Writer = UserCurrent.UserName;
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementRandom", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementRandom(int id)
        {
            var ARandom = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "R")
                .SingleOrDefault();
            if(ARandom == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                return View(ARandom);
            }
            
        }

        [HttpPost]
        public IActionResult EditAnnouncementRandom(Announcement ARandom,int id)
        {
            var random = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "R")
                .SingleOrDefault();
            if(random == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
                random.WriteTime = DateTime.Now;
                random.Writer = UserCurrent.UserName;
                random.Title = ARandom.Title;
                random.Content = ARandom.Content;
                random.PublicTime = ARandom.PublicTime;
                random.PublicUnit = ARandom.PublicUnit;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementRandom", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementRandom(int id)
        {
            var ARandom = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "R")
                .SingleOrDefault();
            if(ARandom == null)
            {
                return Content("error");
            }
            else
            {
                DB.Announcement.Remove(ARandom);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion
        #region 经营异常公告
        //经营异常公告
        public IActionResult DetailsAnnouncementUnsual()
        {
            var AUnsual = DB.Announcement
                .Include(x=>x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "U")
                .ToList();
            return PagedView(AUnsual, 10);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementUnsual()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementUnsual(Announcement AUnsual)
        {

            DB.Announcement.Add(AUnsual);
            var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
            AUnsual.WriteTime = DateTime.Now;
            AUnsual.Writer = UserCurrent.UserName;
            AUnsual.TypeId = DB.TypeCS.Where(x => x.NameType == "U").SingleOrDefault().Id;
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementUnsual", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementUnsual(int id)
        {
            var AUnsual = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "U")
                .SingleOrDefault();
            if (AUnsual == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                return View(AUnsual);
            }

        }

        [HttpPost]
        public IActionResult EditAnnouncementUnsual(Announcement AUnsual, int id)
        {
            var Unsual = DB.Announcement
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (Unsual == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
                Unsual.WriteTime = DateTime.Now;
                Unsual.Writer = UserCurrent.UserName;
                Unsual.Title = AUnsual.Title;
                Unsual.Content = AUnsual.Content;
                Unsual.PublicTime = AUnsual.PublicTime;
                Unsual.PublicUnit = AUnsual.PublicUnit;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementUnsual", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementUnsual(int id)
        {
            var AUnsual = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "U")
                .SingleOrDefault();
            if (AUnsual == null)
            {
                return Content("error");
            }
            else
            {
                DB.Announcement.Remove(AUnsual);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion
        #region 严重违法公告
        //严重违法公告
        public IActionResult DetailsAnnouncementIllegal()
        {
            var AIllegal = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
                .OrderByDescending(x => x.WriteTime)
                .ToList();
            return PagedView(AIllegal, 10);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementIllegal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementIllegal(Announcement AIllegal)
        {

            DB.Announcement.Add(AIllegal);
            var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
            AIllegal.TypeId = DB.TypeCS.Where(x => x.NameType == "I").SingleOrDefault().Id;
            AIllegal.WriteTime = DateTime.Now;
            AIllegal.Writer = UserCurrent.UserName;
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementIllegal", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementIllegal(int id)
        {
            var AIllegal = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "I")
                .SingleOrDefault();
            if (AIllegal == null)
            {
                return Content("不存在该记录");
            }
            else
            {

                return View(AIllegal);
            }

        }

        [HttpPost]
        public IActionResult EditAnnouncementIllegal(Announcement AIllegal, int id)
        {
            var Illegal = DB.Announcement
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (Illegal == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
                Illegal.WriteTime = DateTime.Now;
                Illegal.Writer = UserCurrent.UserName;
                Illegal.Title = AIllegal.Title;
                Illegal.Content = AIllegal.Content;
                Illegal.PublicTime = AIllegal.PublicTime;
                Illegal.PublicUnit = AIllegal.PublicUnit;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementIllegal", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementIllegal(int id)
        {
            var AIllegal = DB.Announcement
                .Where(x => x.Id == id && x.TypeCS.NameType == "I")
                .SingleOrDefault();
            if (AIllegal == null)
            {
                return Content("error");
            }
            else
            {
                DB.Announcement.Remove(AIllegal);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion

        #region  抽查公示填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoRandom()
        {
            var infoRandom = DB.Info
                .Include(x=>x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Rin")
                .OrderByDescending(x=>x.InTime)
                .ToList();
            return PagedView(infoRandom, 10);
        }
        
        [HttpGet]
        public IActionResult CreateInfoRandom()//添加表格内容
        {

            var RType = DB.TypeCS
                .Where(x=>x.NameType == "RType")
                .ToList();
            ViewBag.RType = RType;
            return View();
        }
       
        [HttpPost]
        public IActionResult CreateInfoRandom(Info infoRandom)
        {
            var baseinfo = DB.BaseInfo
                .Where(x => x.RegisteNumber == infoRandom.RegisteNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                infoRandom.TypeId = DB.TypeCS.Where(x => x.NameType == "Rin").SingleOrDefault().Id;
                infoRandom.CompanyName = baseinfo.CompanyName;
                infoRandom.WriteName = User.Current.UserName;
                infoRandom.WriteTime = DateTime.Now;
                DB.Info.Add(infoRandom);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
        }
        
        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoRandom(int id)
        {
            var infoRandom = DB.Info
                .Include(x=>x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Rin")
                .SingleOrDefault();
            if (infoRandom == null)
                return Content("查无此人");
            else
            {
                var RType = DB.TypeCS
                    .Where(x => x.Types != infoRandom.Result)
                    .Where(x=>x.NameType == "RType")
                    .ToList();
                ViewBag.RType = RType;
                return View(infoRandom);
            }
                
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoRandom(int id, Info infoRandom)
        {
            var info = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Rin")
                .SingleOrDefault();
            if(infoRandom == null)
                return Content("没有这个id记录");
            else
            {          
                var b = DB.BaseInfo
                    .Where(x => x.RegisteNumber == infoRandom.RegisteNumber)
                    .SingleOrDefault();
                if(b== null)
                {
                    return Content("no");
                }
                else
                {
                    info.RegisteNumber = infoRandom.RegisteNumber;
                    info.CompanyName = b.CompanyName;
                    info.PublicUnit = infoRandom.PublicUnit;
                    info.InTime = infoRandom.InTime;
                    info.Result = infoRandom.Result;
                    info.WriteTime = DateTime.Now;
                    info.WriteName = User.Current.UserName;
                    DB.SaveChanges();
                    return Content("success");
                }               
            }
        }
       
        public IActionResult DeleteInfoRandom(int id)
        {
            var infoRandom = DB.Info
                .Include(x=>x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Rin")
                .SingleOrDefault();
            if(infoRandom == null)
            {
                return Content("null");
            }
            else
            {
                DB.Info.Remove(infoRandom);
                DB.SaveChanges();
                return Content("success");
            }
            
        }
        #endregion
        #region  严重违法公示填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoIllegal()
        {
            var InfoIllegal = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Iin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            return PagedView(InfoIllegal, 10);
        }

        [HttpGet]
        public IActionResult CreateInfoIllegal()//添加表格内容
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInfoIllegal(Info infoIllegal)
        {
            var baseinfo = DB.BaseInfo
                .Where(x => x.RegisteNumber == infoIllegal.RegisteNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                infoIllegal.TypeId = DB.TypeCS.Where(x => x.NameType == "Iin").SingleOrDefault().Id;
                infoIllegal.CompanyName = baseinfo.CompanyName;
                infoIllegal.WriteName = User.Current.UserName;
                infoIllegal.WriteTime = DateTime.Now;
                DB.Info.Add(infoIllegal);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
        }

        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoIllegal(int id)
        {
            var infoIllegal = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Iin")
                .SingleOrDefault();
            if (infoIllegal == null)
                return Content("查无此人");
            else
                return View(infoIllegal);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoIllegal(int id, Info infoIllegal)
        {
            var info = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Iin")
                .SingleOrDefault();
            if (infoIllegal == null)
                return Content("没有这个id记录");
            else
            {
                var b = DB.BaseInfo
                    .Where(x => x.RegisteNumber == info.RegisteNumber)
                    .SingleOrDefault();
                if (b == null)
                {
                    return Content("RegisterNo");
                }
                else
                {
                    info.RegisteNumber = infoIllegal.RegisteNumber;
                    info.CompanyName = b.CompanyName;
                    info.InTime = infoIllegal.InTime;
                    info.WriteName = User.Current.UserName;
                    info.WriteTime = DateTime.Now;
                    info.InReason = infoIllegal.InReason;
                    info.PublicUnit = infoIllegal.PublicUnit;
                    DB.SaveChanges();
                    return Content("success");
                }            
            }
        }
        public IActionResult DeleteInfoIllegal(int id)
        {
            var infoIllegal = DB.Info
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoIllegal == null)
            {
                return Content("null");
            }
            else
            {
                DB.Info.Remove(infoIllegal);
                DB.SaveChanges();
                return Content("success");
            }
        }
        #endregion

        #region  经营异常公示填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoUnusual()
        {
            var InfoUnusual = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Uin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            return PagedView(InfoUnusual, 10);
        }

        [HttpGet]
        public IActionResult CreateInfoUnusual()//添加表格内容
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInfoUnusual(Info infoUnusual)
        {
            var baseinfo = DB.BaseInfo
                .Where(x => x.RegisteNumber == infoUnusual.RegisteNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                infoUnusual.TypeId = DB.TypeCS.Where(x => x.NameType == "Uin").SingleOrDefault().Id;
                infoUnusual.CompanyName = baseinfo.CompanyName;
                infoUnusual.WriteName = User.Current.UserName;
                infoUnusual.WriteTime = DateTime.Now;
                DB.Info.Add(infoUnusual);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
        }

        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoUnusual(int id)
        {
            var infoUnusual = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Uin")
                .SingleOrDefault();
            if (infoUnusual == null)
                return Content("查无此人");
            else
                return View(infoUnusual);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoUnusual(int id, Info infoUnusual)
        {
            var info = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == "Uin")
                .SingleOrDefault();
            if (infoUnusual == null)
                return Content("没有这个id记录");
            else
            {
                var b = DB.BaseInfo
                    .Where(x => x.RegisteNumber == info.RegisteNumber)
                    .SingleOrDefault();
                if (b == null)
                {
                    return Content("RegisterNo");
                }
                else
                {
                    info.InTime = infoUnusual.InTime;
                    info.RegisteNumber = infoUnusual.RegisteNumber;
                    info.CompanyName = b.CompanyName;
                    info.WriteName = User.Current.UserName;
                    info.WriteTime = DateTime.Now;
                    info.InReason = infoUnusual.InReason;
                    info.PublicUnit = infoUnusual.PublicUnit;
                    DB.SaveChanges();
                    return Content("success");
                }       
            }
        }
        public IActionResult DeleteInfoUnusual(int id)
        {
            var infoUnusual = DB.Info
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoUnusual == null)
            {
                return Content("null");
            }
            else
            {
                DB.Info.Remove(infoUnusual);
                DB.SaveChanges();
                return Content("success");
            }

        }
        #endregion
        #region 企业年度报表
        //查看企业详情
        [HttpGet]
        public IActionResult ConcertDYRE(int id)
        {
            var report = DB.YearReportEnterprise
                .Include(x=>x.User)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(report == null)
            {
                return Content("没有该年度报表");
            }
            else
            {
                return View(report);
            }
        } 
        //创建
        [HttpGet]
        public IActionResult DetailsYearReportEnterprise()
        {
            var yreport = DB.YearReportEnterprise
                .Include(x => x.User)
                .OrderByDescending(x=>x.Id)
                .ToList();
            ViewBag.Count = yreport.Count();
            return PagedView(yreport,10);
        }
        #endregion

        #region 系统管理
        [HttpGet]
        public IActionResult DetailsType()
        {
            return PagedView(DB.TypeCS, 10); 
        }
        #endregion
    }
}
