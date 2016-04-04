using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using credit.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
    [Authorize (Roles ="管理员")]
    public class AdminController : Controller
    {
        [FromServices]
        public CreditContext DB { get; set; }

        #region 管理员添加的基本信息（增删改查）
        [HttpGet]
        public IActionResult DetailsBaseInfo()
        {
            return View(DB.BaseInfo);
        }
        
        [HttpGet]
        public IActionResult CreateBaseInfo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBaseInfo(BaseInfo baseInfo)
        {
            DB.BaseInfo.Add(baseInfo);
            DB.SaveChanges();
            return RedirectToAction("DetailsBaseInfo", "Admin");
        }
        [HttpGet]
        public IActionResult EditBaseInfo(int id)
        {
            var baseInfo = DB.BaseInfo
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (baseInfo == null)
                return Content("查无此人");
            else
                return View(baseInfo);
        }
        
        [HttpPost]
        public IActionResult EditBaseInfo(int id, BaseInfo BaseInfo)
        {
            var baseInfo = DB.BaseInfo
                .Where(x => x.Id == id)
                .SingleOrDefault();
            baseInfo.EnterpriseName = BaseInfo.EnterpriseName;
            baseInfo.RegistrationNumber = BaseInfo.RegistrationNumber;
            DB.SaveChanges();
            return RedirectToAction("DetailsBaseInfo", "Admin");

        }
        #endregion

        //删除基本信息
        public IActionResult DeteleBaseInfo(int id)
        {
            var baseInfo = DB.BaseInfo
                .Where(x => x.Id == id)
                .SingleOrDefault();
            DB.BaseInfo.Remove(baseInfo);
            DB.SaveChanges();
            System.Diagnostics.Debug.Write("id=" + id);
            
            return RedirectToAction("DetailsBaseInfo","Admin");
        }
        
        #region //抽查填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoRandom()
        {
            return View(DB.InfoRandom);
        }
        
        [HttpGet]
        public IActionResult CreateInfoRandom()//添加表格内容
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult CreateInfoRandom(InfoRandom infoRandom)
        {
            //判断 抽查时填入的注册号 在数据库中是否存在
            //下面这样写可以判断他在 baseInfo 中是否存在吗？
            //if (infoRandom.RegistrationNumber == baseInfo.RegistrationNumber)
            if(HttpContext.User.Identity.IsAuthenticated)//判断用户是否登陆
            {
                var user = DB.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();//找到当前用户
                var baseinfo = DB.BaseInfo.Where(x => x.RegistrationNumber == infoRandom.RegistrationNumber).SingleOrDefault();
                if (baseinfo != null)
                {
                    DB.InfoRandom.Add(infoRandom);
                    DB.SaveChanges();
                }
                else
                {
                    return Content("注册号不存在，请检查");//需要用异步
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return Content("添加成功");//需要用异步
        }
        
        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoRandom(int id)
        {
            var infoRandom = DB.InfoRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoRandom == null)
                return Content("查无此人");
            else
            return View(infoRandom);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoRandom(int id, InfoRandom infoRandom)
        {
            var info = DB.InfoRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(infoRandom == null)
                return Content("没有这个id记录");
            info.DateTime = infoRandom.DateTime;
            info.Result = infoRandom.Result;
            
            DB.SaveChanges();
            return RedirectToAction("DetailsInfoRandom", "Admin");

        }
       
        /*public IActionResult DeleteInfoRandom(int id)
        {
            var infoRandom = DB.InfoRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            DB.InfoRandom.Remove(infoRandom);
            DB.SaveChanges();
            System.Diagnostics.Debug.Write("id=" + id);
            return RedirectToAction("DetailsInfoRandom", "Admin");
        }*/
        #endregion
    }
}
