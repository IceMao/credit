using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

namespace credit.Controllers
{
    public class AccountController : BaseController
    {
        
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username,string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password,false, false);
            if (result.Succeeded)
            {
                if (User.IsInRole("管理员"))
                {
                    return RedirectToAction("Manage", "Home"); 
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return Content("登录失败");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            if (User.IsInRole("管理员"))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        [HttpGet]
        public IActionResult Modify()
        {
            return View();//??当前用户修改密码
        }
    }
}
