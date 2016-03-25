using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace credit.Controllers
{
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

        //抽查填写
        #region 显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoRandom()
        {
            return View(DB.InfoRandom);
        }
        #endregion

        #region 添加表格内容
        [HttpGet]
        public IActionResult CreateInfoRandom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateInfoRandom(InfoRandom infoRandom)
        {
            //判断 抽查时填入的注册号 在数据库中是否存在
            //下面这样写可以判断他在 baseInfo 中是否存在吗？
            
            //if(infoRandom.RegistrationNumber == baseInfo.RegistrationNumber)
            
            return RedirectToAction("DetailsInfoRandom", "Admin");
        }
        
        #endregion

        #region 编辑表格
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
        #endregion

        #region
        
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
