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

        #region 登录
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username,string password)
        {
            var result = await SignInManager.PasswordSignInAsync(username, password,false, false);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(username);
                if(await UserManager.IsInRoleAsync(user, "管理员"))
                {
                    return Content("管理员");
                }
                else
                {
                    return Content("联络员");
                }
                //if (User.IsInRole("管理员"))
                //{
                //    return Content("管理员"); 
                //}
                //else
                //{
                //    return Content("联络员");
                //}
            }
            else
            {
                return Content("登录失败");
            }
        }
        #endregion
        #region 登出
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
            if (UserCurrent.Level == "99" || UserCurrent.Level=="10")
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
        #region 修改密码
        [Authorize(Roles ="管理员")]
        [HttpGet]
        public IActionResult Modify()
        {
            return View();
        }
        [Authorize(Roles = "管理员")]
        [HttpPost]
        public async Task<IActionResult> Modify(string password, string NewPassword)
        {
            var UserCurrent = DB.Users
                    .Where(x => x.UserName == HttpContext.User.Identity.Name)
                    .SingleOrDefault();
            if (UserCurrent == null)
            {
                return Content("error");
            }
            else
            {
                await UserManager.ChangePasswordAsync(UserCurrent, password, NewPassword);
                await SignInManager.SignOutAsync();
                return Content("success");
            }
        }
        #endregion
        #region 联络员注册
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string RegistrationNumber,string username,string password,string PhoneNumber,string LiaisonIdNumber,string RealName)
        {
            var register = DB.BaseInfo//判断注册号是不是存在
                .Where(x => x.RegistrationNumber == RegistrationNumber)
                .SingleOrDefault();
            if(register == null)
            {
                return Content("nullRegisterN");
            }
            else
            {
                var reg = DB.Users//判断该注册号是否有联络员
                    .Where(x => x.RegistrationNumber == RegistrationNumber)
                    .SingleOrDefault();
                if(reg == null)
                {
                    var user = DB.Users
                        .Where(x => x.UserName == username)
                        .SingleOrDefault();
                    if(user == null)
                    {
                        var Register = new User
                        {
                            RegistrationNumber = RegistrationNumber,
                            UserName = username,
                            PhoneNumber = PhoneNumber,
                            LiaisonIdNumber = LiaisonIdNumber,
                            RealName = RealName,
                            Level = "1",
                            EnterpriseName = register.EnterpriseName,
                        };
                        await UserManager.CreateAsync(Register, password);
                        await UserManager.AddToRoleAsync(Register, "联络员");
                        DB.SaveChanges();
                        return Content("success");
                        
                    }
                    else
                    {
                        return Content("usernameHave");
                    }
                    
                }
                else
                {
                    return Content("have");
                }
            }
        } 
        #endregion
    }
}
