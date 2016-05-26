using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

namespace credit.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult Index1()
        {

            return View();
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Manage()
        {
            var aR = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "R")
                .ToList();
            ViewBag.R = aR.Count();
            var aU = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "U")
                .ToList();
            ViewBag.U = aU.Count();
            var aI = DB.Announcement
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "I")
                .ToList();
            ViewBag.I = aI.Count();

            var infoR = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Rin")
                .ToList();
            ViewBag.infoR = infoR.Count();
            var infoU = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Uin")
                .ToList();
            ViewBag.infoU = infoU.Count();
            var infoI = DB.Info
                .Include(x => x.TypeCS)
                .Where(x => x.TypeCS.NameType == "Iin")
                .ToList();
            ViewBag.infoI = infoI.Count();
            return View();
        }
    }
}
