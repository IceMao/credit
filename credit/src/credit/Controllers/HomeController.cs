using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

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
        [HttpGet]
        public IActionResult Index2()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Index3()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Index4()
        {

            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }
    }
}
