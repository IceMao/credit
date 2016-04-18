﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Identity;

namespace credit.Controllers
{
    
    public class BaseController : Controller
    {
        [FromServices]
        public CreditContext DB { get; set; }
        [FromServices]
        public UserManager<User> userManager { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
                ViewBag.UserCurrent = UserCurrent;
            }
        }

    }
}
