using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    public class PageController : Controller
    {
        [FromServices]
        public CreditContext DB { get; set; }

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
        //异常公告
        [HttpGet]
        public IActionResult AnnouncementUnsual()
        {
            return View();
        }
        //AnnouncementRandom 抽查检查信息公告
        [HttpGet]
        public IActionResult AnnouncementRandom()
        {
            return View();
        }
        //AnnouncementIllegal 严重违法信息公告
        [HttpGet]
        public IActionResult AnnouncementIllegal()
        {
            return View();
        }
        #endregion

        #region 各个公示 显示 页面

        [HttpGet]
        public IActionResult InfoRandom()
        {
            return View();
        }
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
