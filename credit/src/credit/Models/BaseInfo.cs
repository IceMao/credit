using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class BaseInfo
    {
        //最基本的信息，由管理员添加，当联络员注册输入注册号时判断 BaseInfo中是否存在
        public int Id { get; set; }
        public string RegisteNumber { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }//住所
        public DateTime BeginTime { get; set; }//营业期限自
        public DateTime EndTime { get; set; }//营业期限至
        public string Scope { get; set; }//经营范围
        public string PublicUnit { get; set; }//登记机关
        public DateTime ApproveTime { get; set; }//核准日期
        [ForeignKey("TypeCS")]
        public int TypeId { get; set; }//basein
        public virtual TypeCS TypeCS { get; set; }
    }
}
