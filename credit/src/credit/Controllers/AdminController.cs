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

        #region 显示表格内容
        [HttpGet]
        public IActionResult DetailsMessage()
        {
            return View(DB.Message);
        }
        #endregion

        #region 添加表格内容
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            DB.Message.Add(message);
            DB.SaveChanges();
            return RedirectToAction("DetailsMessage", "Admin");
        }
        #endregion

        #region 编辑表格
        [HttpGet]
        public IActionResult EditMessage(int id)
        {
            var message = DB.Message
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (message == null)
                return Content("查无此人");
            else
            return View(message);
        }

        //处理编辑表格请求
        [HttpPost]
        public IActionResult EditMessage(int id, Message message)
        {
            var msg = DB.Message
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(msg == null)
                return Content("没有这个id记录");
            msg.RegistrationNumber = message.RegistrationNumber;
            msg.DateTime = message.DateTime;
            msg.EnterpriseAddress = message.EnterpriseAddress;
            msg.EnterpriseEmail = message.EnterpriseEmail;
            msg.EnterpriseName = message.EnterpriseName;
            msg.EnterpriseTel = message.EnterpriseTel;
            msg.ZipCode = message.ZipCode;
            DB.SaveChanges();
            return RedirectToAction("DetailsMessage", "Admin");

        }
        #endregion

        #region
        
        public IActionResult DeleteMessage(int id)
        {
            var message = DB.Message
                .Where(x => x.Id == id)
                .SingleOrDefault();
            DB.Message.Remove(message);
            DB.SaveChanges();
            System.Diagnostics.Debug.Write("id=" + id);
            return RedirectToAction("DetailsMessage", "Admin");
        }
        

        #endregion
    }
}
