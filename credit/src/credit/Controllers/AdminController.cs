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

        #region 管理员添加 注册号基本表（增删改查）
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
            var oldBase = DB.BaseInfo
                .Where(x => x.RegistrationNumber == baseInfo.RegistrationNumber)
                .SingleOrDefault();
            if (oldBase != null)
            {
                return Content("error");
            }
            else
            {
                DB.BaseInfo.Add(baseInfo);
                DB.SaveChanges();
                //return RedirectToAction("DetailsBaseInfo", "Admin");
                return Content("success");
            }
            
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
        //删除基本信息
        public IActionResult DeleteBaseInfo(int id)
        {
            var baseInfo = DB.BaseInfo
                .Where(x => x.Id == id)
                .SingleOrDefault();
            DB.BaseInfo.Remove(baseInfo);
            DB.SaveChanges();

            return Content("success");
        }
        #endregion

        #region 抽查公告
        //抽查公告
        public IActionResult DetailsAnnouncementRandom()
        {
            var ARandom = DB.AnnouncementRandom.ToList();
            return View(ARandom);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementRandom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementRandom(AnnouncementRandom ARandom)
        {

            DB.AnnouncementRandom.Add(ARandom);
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementRandom", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementRandom(int id)
        {
            var ARandom = DB.AnnouncementRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(ARandom == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                return View(ARandom);
            }
            
        }

        [HttpPost]
        public IActionResult EditAnnouncementRandom(AnnouncementRandom ARandom,int id)
        {
            var random = DB.AnnouncementRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(random == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                random.title = ARandom.title;
                random.Content = ARandom.Content;
                random.DateTime = ARandom.DateTime;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementRandom", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementRandom(int id)
        {
            var ARandom = DB.AnnouncementRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(ARandom == null)
            {
                return Content("error");
            }
            else
            {
                DB.AnnouncementRandom.Remove(ARandom);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion
        #region 经营异常公告
        //经营异常公告
        public IActionResult DetailsAnnouncementUnsual()
        {
            var AUnsual = DB.AnnouncementUnsual.ToList();
            return View(AUnsual);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementUnsual()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementUnsual(AnnouncementUnsual AUnsual)
        {

            DB.AnnouncementUnsual.Add(AUnsual);
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementUnsual", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementUnsual(int id)
        {
            var AUnsual = DB.AnnouncementUnsual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (AUnsual == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                return View(AUnsual);
            }

        }

        [HttpPost]
        public IActionResult EditAnnouncementUnsual(AnnouncementUnsual AUnsual, int id)
        {
            var Unsual = DB.AnnouncementUnsual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (Unsual == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                Unsual.title = AUnsual.title;
                Unsual.Content = AUnsual.Content;
                Unsual.DateTime = AUnsual.DateTime;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementUnsual", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementUnsual(int id)
        {
            var AUnsual = DB.AnnouncementUnsual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (AUnsual == null)
            {
                return Content("error");
            }
            else
            {
                DB.AnnouncementUnsual.Remove(AUnsual);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion
        #region 严重违法公告
        //严重违法公告
        public IActionResult DetailsAnnouncementIllegal()
        {
            var AIllegal = DB.AnnouncementIllegal.ToList();
            return View(AIllegal);
        }
        [HttpGet]
        public IActionResult CreateAnnouncementIllegal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnnouncementIllegal(AnnouncementIllegal AIllegal)
        {

            DB.AnnouncementIllegal.Add(AIllegal);
            DB.SaveChanges();
            //return Content("success");
            return RedirectToAction("DetailsAnnouncementIllegal", "Admin");
        }

        [HttpGet]
        public IActionResult EditAnnouncementIllegal(int id)
        {
            var AIllegal = DB.AnnouncementIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (AIllegal == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                return View(AIllegal);
            }

        }

        [HttpPost]
        public IActionResult EditAnnouncementIllegal(AnnouncementIllegal AIllegal, int id)
        {
            var Illegal = DB.AnnouncementIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (Illegal == null)
            {
                return Content("不存在该记录");
            }
            else
            {
                Illegal.title = AIllegal.title;
                Illegal.Content = AIllegal.Content;
                Illegal.DateTime = AIllegal.DateTime;
                DB.SaveChanges();
                return RedirectToAction("DetailsAnnouncementIllegal", "Admin");
            }
        }
        public IActionResult DeleteAnnouncementIllegal(int id)
        {
            var AIllegal = DB.AnnouncementIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (AIllegal == null)
            {
                return Content("error");
            }
            else
            {
                DB.AnnouncementIllegal.Remove(AIllegal);
                DB.SaveChanges();
                return Content("success");
            }
        }

        #endregion

        #region  抽查公示填写
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


            //if(HttpContext.User.Identity.IsAuthenticated)//判断用户是否登陆
            //{
            //    var user = DB.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();//找到当前用户
            //    var baseinfo = DB.BaseInfo.Where(x => x.RegistrationNumber == infoRandom.RegistrationNumber).SingleOrDefault();
            //    if (baseinfo != null)
            //    {
            //        DB.InfoRandom.Add(infoRandom);
            //        DB.SaveChanges();
            //    }
            //    else
            //    {
            //        return Content("注册号不存在，请检查");//需要用异步
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //return Content("添加成功");//需要用异步 

            var baseinfo = DB.BaseInfo
                .Where(x => x.RegistrationNumber == infoRandom.RegistrationNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                DB.InfoRandom.Add(infoRandom);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
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
       
        public IActionResult DeleteInfoRandom(int id)
        {
            var infoRandom = DB.InfoRandom
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(infoRandom == null)
            {
                return Content("null");
            }
            else
            {
                DB.InfoRandom.Remove(infoRandom);
                DB.SaveChanges();
                return Content("success");
            }
            
        }
        #endregion
        #region  严重违法公示填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoIllegal()
        {
            return View(DB.InfoIllegal);
        }

        [HttpGet]
        public IActionResult CreateInfoIllegal()//添加表格内容
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInfoIllegal(InfoIllegal infoIllegal)
        {


            var baseinfo = DB.BaseInfo
                .Where(x => x.RegistrationNumber == infoIllegal.RegistrationNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                DB.InfoIllegal.Add(infoIllegal);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
        }

        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoIllegal(int id)
        {
            var infoIllegal = DB.InfoIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoIllegal == null)
                return Content("查无此人");
            else
                return View(infoIllegal);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoIllegal(int id, InfoIllegal infoIllegal)
        {
            var info = DB.InfoIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoIllegal == null)
                return Content("没有这个id记录");
            info.DateTime = infoIllegal.DateTime;

            DB.SaveChanges();
            return RedirectToAction("DetailsInfoIllegal", "Admin");

        }

        public IActionResult DeleteInfoIllegal(int id)
        {
            var infoIllegal = DB.InfoIllegal
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoIllegal == null)
            {
                return Content("null");
            }
            else
            {
                DB.InfoIllegal.Remove(infoIllegal);
                DB.SaveChanges();
                return Content("success");
            }

        }
        #endregion

        #region  经营异常公示填写
        //显示表格内容
        [HttpGet]
        public IActionResult DetailsInfoUnusual()
        {
            return View(DB.InfoUnusual);
        }

        [HttpGet]
        public IActionResult CreateInfoUnusual()//添加表格内容
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInfoUnusual(InfoUnusual infoUnusual)
        {
            var baseinfo = DB.BaseInfo
                .Where(x => x.RegistrationNumber == infoUnusual.RegistrationNumber)
                .SingleOrDefault();
            if (baseinfo != null)
            {
                DB.InfoUnusual.Add(infoUnusual);
                DB.SaveChanges();
                return Content("success");
            }
            else
            {
                return Content("error");//需要用异步
            }
        }

        // 编辑表格
        [HttpGet]
        public IActionResult EditInfoUnusual(int id)
        {
            var infoUnusual = DB.InfoUnusual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoUnusual == null)
                return Content("查无此人");
            else
                return View(infoUnusual);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditInfoUnusual(int id, InfoUnusual infoUnusual)
        {
            var info = DB.InfoUnusual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoUnusual == null)
                return Content("没有这个id记录");
            info.DateTime = infoUnusual.DateTime;

            DB.SaveChanges();
            return RedirectToAction("DetailsInfoUnusual", "Admin");

        }

        public IActionResult DeleteInfoUnusual(int id)
        {
            var infoUnusual = DB.InfoUnusual
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (infoUnusual == null)
            {
                return Content("null");
            }
            else
            {
                DB.InfoUnusual.Remove(infoUnusual);
                DB.SaveChanges();
                return Content("success");
            }

        }
        #endregion
        #region 企业年度报表
        //创建

        [HttpGet]
        public IActionResult DetailsYearReportEnterprise()
        {

            return View();
        }
        
        [HttpPost]
        public IActionResult DetailsYearReportEnterprise(YearReportEnterprise YRE,int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)//判断用户是否登陆
            {
                var user = DB.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();//找到当前用户
                //注册号从当前用户读取
                var yre = DB.YearReportEnterprise
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                /*var baseinfo = DB.BaseInfo.Where(x => x.RegistrationNumber == infoRandom.RegistrationNumber).SingleOrDefault();
                if (baseinfo != null)
                {
                    DB.InfoRandom.Add(infoRandom);
                    DB.SaveChanges();
                }
                else
                {
                    return Content("注册号不存在，请检查");//需要用异步
                }*/
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return Content("添加成功");//需要用异步
            

        }
        #endregion
    }
}
