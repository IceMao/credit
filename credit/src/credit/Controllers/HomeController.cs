using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Models
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }
    }
}
