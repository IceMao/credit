﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class User:IdentityUser<long>
    {
        public string RealName { get; set; }
        public string Level { get; set; } //管理员等级 99 / 10

        public DateTime RegisterTime { get; set; }//注册时间

        public string RegisteNumber { get; set; }//注册号
        public string CompanyName { get; set; }
        public string LiaisonIdNumber { get; set; }//联系人身份证号
        public string LegalIdNumber { get; set; }//法定代表人证件号
        
        //public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
