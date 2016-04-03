using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    public class AccountController : Controller
    {
        [FromServices]
        public SignInManager<User> signInManager { get; set; }
        
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
                return Content("success");
                // return RedirectToAction("Manage", "Home");//后台管理页面
            }
            else
            {
                
                return RedirectToAction("Login", "Account");
               
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Registe()
        {
            return View();
        }/*
        [HttpPost]
        public async Task<IActionResult> Registe(string username, string password)
        {
            var user = new User
            {
                UserName = username,
            };
            //创建用户
            var result = ?;
            if (!result.Succeeded)
            {
                return Content(result.Errors.First().Description);
            }
            return RedirectToAction("Login", "Account");
        }*/
        [HttpGet]
        public IActionResult LiaisonRegiste()
        {
            return View();
        }/*
        [HttpPost]
        public async Task<IActionResult> LiaisonRegiste(string registrationNumber, string liaisonName, string liaisonIdNumber, string legalIdNumber, string cellPhoneNumber)
        {
            var liaison = new User
            {
                RegistrationNumber = registrationNumber,
                LiaisonName = liaisonName,
                LiaisonIdNumber = liaisonIdNumber,
                LegalIdNumber = legalIdNumber,
                CellPhoneNumber = cellPhoneNumber
            };
            var result = await UserManager.CreateAsync(User, password);
            if (!result.Succeeded)
            {
                return Content(result.Errors.First().Description);
            }
            return RedirectToAction("Index", "Home");//网站首页
        }*/
        [HttpGet]
        public IActionResult Modify()
        {
            return View();//??当前用户修改密码
        }
    }
}
