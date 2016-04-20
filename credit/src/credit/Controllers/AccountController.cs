using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;
using Microsoft.AspNet.Identity;

namespace credit.Controllers
{
    public class AccountController : BaseController
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
            }
            else
            {
                return RedirectToAction("Index", "Home");
             }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #region 联络员
        [HttpGet]
        public IActionResult LoginLiaison()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginLiaison(string username, string password)
        {
            var user = await signInManager.PasswordSignInAsync(username, password, false, false);
            if (user.Succeeded)
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }
        //注册
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.liaison = (await userManager.GetUsersInRoleAsync("联络员")).Count();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username,string password,string EnterpriseName, string RegistrationNumber, string LiaisonName,string LiaisonIdNumber,string CellPhoneNumber)
        {
            
            var register = DB.User
                .Where(x => x.RegistrationNumber == RegistrationNumber)
                .SingleOrDefault();
            if(register == null)
            {
                return Content("error");
            }
            else
            {
                var user = new User
                {
                    UserName = username,
                    EnterpriseName = EnterpriseName,
                    RegistrationNumber = RegistrationNumber,
                    LiaisonIdNumber = LiaisonIdNumber,
                    LiaisonName = LiaisonName,
                    CellPhoneNumber = CellPhoneNumber
                };
                //创建用户
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "联络员");
                DB.SaveChanges();
                return Content("success");
            }
            
        }
        #endregion
        [HttpGet]
        public IActionResult Modify()
        {
            return View();//??当前用户修改密码
        }
    }
}
