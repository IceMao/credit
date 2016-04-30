using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    public class PageController : BaseController
    {
        #region
        public IActionResult Error()
        {
            return View();
        }
        #endregion
        #region 企业年度信息填报
        [Authorize]
        [HttpGet]
        public IActionResult CreateYearReportEnterprise()
        {
            //已经登录
            var UserCurrent = DB.User
                .Where(x => x.UserName == HttpContext.User.Identity.Name)
                .Where(x=>x.Level == "1" )//是 联络员
                .SingleOrDefault();

            if (UserCurrent.Level == "1")//是联络员
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var type = DB.OperatStateType
                        .OrderBy(x => x.Id)
                        .ToList();
                    ViewBag.RNumber = UserCurrent.RegistrationNumber;
                    ViewBag.EName = UserCurrent.EnterpriseName;
                    ViewBag.OType = type;
                    return View();
                }
                else
                {
                    return Content("请联络员先登录");
                }
                
            }
            else
            {
                return Content("不是联络员，请联络员登录");

            }
        }
        [Authorize(Roles = ("联络员"))]
        [HttpPost]
        public IActionResult CreateYearReportEnterprise(string registerNumber,DateTime DateTime,string enterpriseName,string tel,string email,string address,string zipCode,string webSite,string investement,string operatState,int employeeNum)
        {
            var table = new YearReportEnterprise
            {
                Tel = tel,
                Email = email,
                DateTime = DateTime,
                Address = address,
                ZipCode = zipCode,
                Website = webSite,
                Investment = investement,
                EmployeeNum = employeeNum,
                OperatState = operatState,
            };
            DB.SaveChanges();
            return Content("success");
        }
        #endregion
        #region 信息公告总页

        [HttpGet]
        public IActionResult Announcement()
        {
            var random = DB.AnnouncementRandom
                .OrderByDescending(x => x.DateTime)
                .ToList();
            ViewBag.random = random;

            var illegal = DB.AnnouncementIllegal
                .OrderByDescending(x => x.DateTime)
                .ToList();
            ViewBag.illegal = illegal;

            var unusual = DB.AnnouncementUnsual
                .OrderByDescending(x => x.DateTime)
                .ToList();
            ViewBag.unusual = unusual;

            return View();
        }
        #endregion



        #region index search

        public IActionResult Search(string key)
        {
            var r = DB.BaseInfo
                .Where(x => x.RegistrationNumber == key)
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

        #region “总页”
        //信息填报
        [HttpGet]
        public IActionResult FillIn()
        {
            return View();
        }
        
        #endregion

        #region 各个公告页面

        //单独公告显示——抽查
        [HttpGet]
        public IActionResult ArticlePageRandom(int id)
        {
            var article = DB.AnnouncementRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(article);
        }
        //单独公告显示——经营异常
        [HttpGet]
        public IActionResult ArticlePageUnusual(int id)
        {
            var article = DB.AnnouncementUnsual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(article);
        }
        //单独公告显示——严重违法
        [HttpGet]
        public IActionResult ArticlePageIllegal(int id)
        {
            var article = DB.AnnouncementIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(article);
        }
        //异常公告
        [HttpGet]
        public IActionResult AnnouncementUnsual()
        {
            var AUnsual = DB.AnnouncementUnsual
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(AUnsual);
        }
        //AnnouncementRandom 抽查检查信息公告
        [HttpGet]
        public IActionResult AnnouncementRandom()
        {
            var ARandom = DB.AnnouncementRandom
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(ARandom);
        }
        //AnnouncementIllegal 严重违法信息公告
        [HttpGet]
        public IActionResult AnnouncementIllegal()
        {
            var AIllegal = DB.AnnouncementIllegal
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(AIllegal);
        }
        #endregion

        #region 抽查公示 显示 页面
        
        [HttpGet]
        public IActionResult InfoRandom()
        {
            var infoRandom = DB.InfoRandom
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(infoRandom);
        }
        
        public IActionResult SearchRandom(string key)
        {

            var enterprise = DB.BaseInfo
                .Where(x => x.RegistrationNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var random = DB.InfoRandom
                    .Where(x => x.RegistrationNumber == key)
                    .ToList();
                return View(random);
            }

        }
        #endregion
        #region 违法公示 显示页面

        [HttpGet]
        public IActionResult InfoIllegal()
        {
            var infoIllegal = DB.InfoIllegal
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(infoIllegal);

        }
        public IActionResult SearchIllegal(string key)
        {

            var enterprise = DB.BaseInfo
                .Where(x => x.RegistrationNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var illegal = DB.InfoIllegal
                    .Where(x => x.RegistrationNumber == key)
                    .ToList();
                return View(illegal);
            }

        }
        #endregion
        #region 异常公示
        [HttpGet]
        public IActionResult InfoUnusual()
        {
            var infoUnusual = DB.InfoUnusual
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return View(infoUnusual);

        }
        public IActionResult SearchUnusual(string key)
        {

            var enterprise = DB.BaseInfo
                .Where(x => x.RegistrationNumber == key)
                .SingleOrDefault();
            if (enterprise == null)
            {
                return RedirectToAction("Error", "Page");
            }
            else
            {
                var unusual = DB.InfoUnusual
                    .Where(x => x.RegistrationNumber == key)
                    .ToList();
                return View(unusual);
                
            }

        }
        #endregion
    }
}
