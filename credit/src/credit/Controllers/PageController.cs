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
        [HttpGet]
        public IActionResult Report()
        {
            return View();
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
                    ViewBag.RNumber = User.Current.RegistrationNumber;
                    ViewBag.EName = User.Current.EnterpriseName;
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
        {//string registerNumber, DateTime DateTime, string enterpriseName, string tel, string email, string address, string zipCode, string webSite, string investement, string operatState, int employeeNum
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
        #region 信息公告总页

        
        #endregion
        #region “信息填报总页”
        //信息填报
        [HttpGet]
        public IActionResult FillIn()
        {
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
                .ToList();
            ViewBag.random = random;

            var illegal = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            ViewBag.illegal = illegal;

            var unusual = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "U")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            ViewBag.unusual = unusual;

            return View();
        }

        //单独公告显示——抽查
        [HttpGet]
        public IActionResult ArticlePageRandom(int id)
        {
            var article = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "R")
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(article);
        }
        //单独公告显示——经营异常
        [HttpGet]
        public IActionResult ArticlePageUnusual(int id)
        {
            var article = DB.Announcement
                 .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "U")
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(article);
        }
        //单独公告显示——严重违法
        [HttpGet]
        public IActionResult ArticlePageIllegal(int id)
        {
            var article = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
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
            return View(AUnsual);
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
            return View(ARandom);
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
            return View(AIllegal);
        }
        #endregion

        #region 抽查公示 显示 页面
        
        [HttpGet]
        public IActionResult InfoRandom()
        {
            var infoRandom = DB.Info
                .Include(x=>x.TypeCS)
                .Where(x=>x.TypeCS.NameType == "Rin")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            return View(infoRandom);
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
                    .Where(x => x.RegisteNumber == key)
                    .ToList();
                return View(random);
            }

        }
        #endregion
        #region 违法公示 显示页面

        [HttpGet]
        public IActionResult InfoIllegal()
        {
            var infoIllegal = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Iin")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            return View(infoIllegal);

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
                    .Where(x => x.RegisteNumber == key)
                    .ToList();
                return View(illegal);
            }

        }
        #endregion
        #region 异常公示
        [HttpGet]
        public IActionResult InfoUnusual()
        {
            var infoUnusual = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Uin")
                .OrderByDescending(x => x.PublicTime)
                .ToList();
            return View(infoUnusual);

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
                    .Where(x => x.RegisteNumber == key)
                    .ToList();
                return View(unusual);
                
            }

        }
        #endregion
    }
}
