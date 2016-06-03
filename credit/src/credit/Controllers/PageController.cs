using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    public class PageController : BaseController
    {
        #region 错误页面
        public IActionResult Error()
        {
            return View();
        }
        #endregion
        #region 企业年度信息填报
        /// <summary>
        /// 游客可以浏览的详情页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OneReport(int id)
        {
            var report = DB.YearReportEnterprise
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(report);
        }
        /// <summary>
        /// 这个report详情页是Report页面点击时的跳转，就是通过联络员查看的详情
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize(Roles = "联络员")]
        [HttpGet]
        public IActionResult DetailOneReport(int id)
        {
            var report = DB.YearReportEnterprise
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .SingleOrDefault();
            return View(report);
        }
        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReportEdit(int id)
        {
            var report = DB.YearReportEnterprise
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .SingleOrDefault();
            var type = DB.TypeCS
                .Where(x => x.Types != report.OperatState && x.NameType == "EType")
                .ToList();
            ViewBag.EType = type;
            return View(report);
        }
        [HttpPost]
        public IActionResult ReportEdit( int id,YearReportEnterprise oldReport)
        {
            var newReport = DB.YearReportEnterprise
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            newReport.Tel = oldReport.Tel;
            newReport.Address = oldReport.Address;
            newReport.ZipCode = oldReport.ZipCode;
            newReport.Email = oldReport.Email;
            newReport.OperatState = oldReport.OperatState;
            newReport.Investment = oldReport.Investment;
            newReport.Website = oldReport.Website;
            newReport.EmployeeNum = oldReport.EmployeeNum;
            DB.SaveChanges();
            return Content("success");
        }
        [Authorize]
        [HttpGet]
        public IActionResult CreateYearReportEnterprise()
        {
            //已经登录           
            if (User.Current.Level == "1")//是联络员
            {
                var report = DB.YearReportEnterprise
                    .Where(x => x.UserId == User.Current.Id && x.DateTime == DateTime.Now.Year)
                    .SingleOrDefault();
                if(report == null)
                {
                    var type = DB.TypeCS
                        .Where(x => x.NameType == "EType")
                        .OrderBy(x => x.Id)
                        .ToList();
                    ViewBag.RNumber = User.Current.RegisteNumber;
                    ViewBag.EName = User.Current.CompanyName;
                    ViewBag.EType = type;
                    return View();
                }
                else
                {
                    return RedirectToAction("Report","Page");
                }

            }
            else
            {
                return Content("不是联络员，请联络员登录");

            }
        }
        [Authorize(Roles = ("联络员"))]
        [HttpPost]
        public IActionResult CreateYearReportEnterprise(YearReportEnterprise report)
        {//string registerNumber, DateTime DateTime, string CompanyName, string tel, string email, string address, string zipCode, string webSite, string investement, string operatState, int employeeNum
            //

            //var report = new YearReportEnterprise
            //{
            //    Tel = tel,
            //    Email = email,
            //    DateTime = DateTime,
            //    Address = address,
            //    ZipCode = zipCode,
            //    Website = webSite,
            //    WriteTime = DateTime.Now.Date,
            //    Investment = investement,
            //    EmployeeNum = employeeNum,
            //    OperatState = operatState,
            //    UserId = User.Current.Id,
            //};
            report.DateTime = DateTime.Now.Year;
            report.WriteTime = DateTime.Now.Date;
            report.UserId = User.Current.Id;
            DB.YearReportEnterprise.Add(report);
            DB.SaveChanges();
            return Content("success");
        }
        #endregion
        #region 信息公示汇总页+公示具体页
        [HttpGet]
        public IActionResult Info()
        {
            var u = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Uin")
                .OrderByDescending(x=>x.InTime)
                .ToList();
            ViewBag.u = u;
            var i = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Iin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            ViewBag.i = i;
            var r = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Rin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            ViewBag.r = r;
            return View();
        }
        
        [HttpGet]
        public IActionResult OneInfo(int id,int typeId)
        {
            var t = DB.TypeCS
                .Where(x=>x.Id == typeId)
                .SingleOrDefault();
            if (t.NameType == "basein")
            {
                var b = DB.BaseInfo
                    .Include(x => x.TypeCS)
                    .Where(x => x.Id == id && t.NameType == "basein")
                    .SingleOrDefault();
                    ViewBag.basein = "basein";
                    ViewBag.rin = "0";
                    ViewBag.uin = "0";
                    ViewBag.iin = "0";

                    ViewBag.infoBase = b;
                    ViewBag.Company = b.CompanyName;
                    ViewBag.Registe = b.RegisteNumber;
                var report = DB.YearReportEnterprise
                    .Include(x => x.User)
                    .Where(x => x.User.RegisteNumber == b.RegisteNumber)
                    .OrderByDescending(x => x.DateTime)
                    .ToList(); 
                ViewBag.report = report;
                    var infoU = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == b.RegisteNumber && x.TypeCS.NameType == "Uin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoU = infoU;
                    if (infoU.Count > 0)
                    {
                        ViewBag.strU = "该企业已列入经营异常名录";
                    }
                    else
                    {
                        ViewBag.strU = "";
                    }
                    var infoR = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == b.RegisteNumber && x.TypeCS.NameType == "Rin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoR = infoR;
                    var infoI = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == b.RegisteNumber && x.TypeCS.NameType == "Iin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoI = infoI;
                if (infoI.Count > 0)
                {
                    ViewBag.strI = "该企业已列入严重违法名录";
                }
                else
                {
                    ViewBag.strI = "";
                }
            }
            else
            {
                var info = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.Id == id && x.TypeCS.NameType == t.NameType)
                .SingleOrDefault();
                ViewBag.Company = info.CompanyName;
                ViewBag.Registe = info.RegisteNumber;
                var report = DB.YearReportEnterprise
                        .Include(x => x.User)
                        .Where(x => x.User.RegisteNumber == info.RegisteNumber)
                        .OrderByDescending(x => x.DateTime)
                        .ToList();
                ViewBag.report = report;
                if (info.TypeCS.NameType == "Uin")
                {
                    ViewBag.uin = "uin";
                    ViewBag.rin = "0";
                    ViewBag.iin = "0";
                    ViewBag.basein = "0";
                    var infoBase = DB.BaseInfo
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber)
                        .SingleOrDefault();
                    ViewBag.infoBase = infoBase;
                    
                    var infoU = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Uin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoU = infoU;
                    if (infoU.Count > 0)
                    {
                        ViewBag.strU = "该企业已列入经营异常名录";
                    }
                    else
                    {
                        ViewBag.strU = "";
                    }

                    var infoR = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Rin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoR = infoR;
                    var infoI = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Iin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoI = infoI;
                    if (infoI.Count > 0)
                    {
                        ViewBag.strI = "该企业已列入严重违法名录";
                    }
                    else
                    {
                        ViewBag.strI = "";
                    }
                }
                else if (info.TypeCS.NameType == "Rin")
                {
                    ViewBag.rin = "rin";
                    ViewBag.uin = "0";
                    ViewBag.iin = "0";
                    ViewBag.basein = "0";
                    var infoBase = DB.BaseInfo
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber)
                        .SingleOrDefault();
                    ViewBag.infoBase = infoBase;
                    var infoU = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Uin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoU = infoU;
                    if (infoU.Count > 0)
                    {
                        ViewBag.strU = "该企业已列入经营异常名录";
                    }
                    else
                    {
                        ViewBag.strU = "";
                    }
                    var infoR = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Rin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoR = infoR;
                    var infoI = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Iin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoI = infoI;
                    if (infoI.Count > 0)
                    {
                        ViewBag.strI = "该企业已列入严重违法名录";
                    }
                    else
                    {
                        ViewBag.strI = "";
                    }
                }
                else if (info.TypeCS.NameType == "Iin")
                {
                    ViewBag.iin = "iin";
                    ViewBag.rin = "0";
                    ViewBag.uin = "0";
                    ViewBag.basein = "0";
                    var infoBase = DB.BaseInfo
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber)
                        .SingleOrDefault();
                    ViewBag.infoBase = infoBase;
                    var infoU = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Uin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoU = infoU;
                    if (infoU.Count > 0)
                    {
                        ViewBag.strU = "该企业已列入经营异常名录";
                    }
                    else
                    {
                        ViewBag.strU = "";
                    }
                    var infoR = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Rin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoR = infoR;
                    var infoI = DB.Info
                        .Include(x => x.TypeCS)
                        .Where(x => x.RegisteNumber == info.RegisteNumber && x.TypeCS.NameType == "Iin")
                        .OrderByDescending(x => x.InTime)
                        .ToList();
                    ViewBag.infoI = infoI;
                    if (infoI.Count > 0)
                    {
                        ViewBag.strI = "该企业已列入严重违法名录";
                    }
                    else
                    {
                        ViewBag.strI = "";
                    }
                }
            }
            
            return View();
        }
        #endregion
        
        #region index search

        public IActionResult Search(string key)
        {
            var r = DB.BaseInfo
                .Where(x => x.RegisteNumber == key)
                .SingleOrDefault();
            if(r == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                return View(r);
            }
        }
        #endregion

        #region 公告页面
        [HttpGet]
        public IActionResult Announcement()
        {
            var random = DB.Announcement
                .Include(x=>x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "R")
                .OrderByDescending(x => x.PublicTime)
                .Take(7)
                .ToList();
            ViewBag.random = random;

            var illegal = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
                .OrderByDescending(x => x.PublicTime)
                .Take(7)
                .ToList();
            ViewBag.illegal = illegal;

            var unusual = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "U")
                .OrderByDescending(x => x.PublicTime)
                .Take(7)
                .ToList();
            ViewBag.unusual = unusual;

            return View();
        }
        //单独公告显示
        [HttpGet]
        public IActionResult Article(int id)
        {
            var article = DB.Announcement
               .Include(x => x.TypeCS)
               .Where(x => x.Id == id)
               .SingleOrDefault();
            return View(article);
        }      
        
        //异常公告
        [HttpGet]
        public IActionResult AnnouncementUnsual()
        {
            var AUnsual = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "U")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            var announcement = DB.Announcement
                .Include(x => x.TypeCS)
                .OrderByDescending(x => x.PublicTime)
                .Take(10)
                .ToList();
            ViewBag.ann = announcement;
            return PagedView(AUnsual, 15);
        }
        //AnnouncementRandom 抽查检查信息公告
        [HttpGet]
        public IActionResult AnnouncementRandom()
        {
            var ARandom = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "R")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            var announcement = DB.Announcement
                .Include(x => x.TypeCS)
                .OrderByDescending(x => x.PublicTime)
                .Take(10)
                .ToList();
            ViewBag.ann = announcement;

            return PagedView(ARandom, 15);
        }
        //AnnouncementIllegal 严重违法信息公告
        [HttpGet]
        public IActionResult AnnouncementIllegal()
        {
            var AIllegal = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            var announcement = DB.Announcement
                .Include(x => x.TypeCS)
                .OrderByDescending(x => x.PublicTime)
                .Take(10)
                .ToList();
            ViewBag.ann = announcement;
            return PagedView(AIllegal, 15);
        }
        #endregion

        #region 抽查公示 显示 页面 和搜索
        
        [HttpGet]
        public IActionResult InfoRandom()
        {
            var infoRandom = DB.Info
                .Include(x=>x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "Rin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            return PagedView(infoRandom, 15);
        }
        
        public IActionResult SearchRandom(string key)
        {
            var enterprise = DB.BaseInfo
                .Where(x => x.RegisteNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var random = DB.Info
                    .Where(x => x.RegisteNumber == key && x.TypeCS.NameType == "Rin")
                    .ToList();
                return View(random);
            }

        }
        #endregion
        #region 违法公示 显示页面 和搜索

        [HttpGet]
        public IActionResult InfoIllegal()
        {
            var infoIllegal = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Iin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            return PagedView(infoIllegal,15);

        }
        public IActionResult SearchIllegal(string key)
        {

            var enterprise = DB.BaseInfo
                .Where(x => x.RegisteNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var illegal = DB.Info
                    .Where(x => x.RegisteNumber == key && x.TypeCS.NameType == "Iin")
                    .ToList();
                return View(illegal);
            }

        }
        #endregion
        #region 异常公示 和搜索
        [HttpGet]
        public IActionResult InfoUnusual()
        {
            var infoUnusual = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Uin")
                .OrderByDescending(x => x.InTime)
                .ToList();
            return PagedView(infoUnusual, 15);

        }
        public IActionResult SearchUnusual(string key)
        {

            var enterprise = DB.BaseInfo
                .Where(x => x.RegisteNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var unusual = DB.Info
                    .Where(x => x.RegisteNumber == key && x.TypeCS.NameType == "Uin")
                    .ToList();
                return View(unusual);
                
            }

        }
        #endregion
    }
}
