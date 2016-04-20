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
    public class PageController : Controller
    {
        [FromServices]
        public CreditContext DB { get; set; }

        #region 企业年度信息填报
        [Authorize(Roles = ("联络员"))]
        [HttpGet]
        public IActionResult CreateYearReportEnterprise()
        {
            return View();
        }
        [Authorize(Roles = ("联络员"))]
        [HttpPost]
        public IActionResult CreateYearReportEnterprise(int XX)
        {
            return View();
        }
        #endregion
        //搜索总页
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        #region “总页”
        //信息填报
        [HttpGet]
        public IActionResult FillIn()
        {
            return View();
        }
        //公告总页
        [HttpGet]
        public IActionResult Announcement()
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
        public IActionResult ArticlePageUnsual(int id)
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

        #region 各个公示 显示 页面

        //抽查公示
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
                return Content("该注册号不存在");
            }
            else
            {
                var random = DB.InfoRandom
                    .Where(x => x.RegistrationNumber == key)
                    .ToList();
                if (random == null)
                {
                    return Content("该注册号不在抽查范围");
                }
                else
                {
                    return View(random);
                }
            }

        }
        //异常公示
        [HttpGet]
        public IActionResult InfoIllegal()
        {
            return View();
        }
        [HttpGet]
        public IActionResult InfoUnusual()
        {
            return View();
        }
        #endregion
    }
}
