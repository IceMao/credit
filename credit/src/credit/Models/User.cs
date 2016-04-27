﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class User:IdentityUser
    {
        public string Level { get; set; } //管理员等级 99 / 10
        public string RegistrationNumber { get; set; }//注册号
        public string EnterpriseName { get; set; }
        public string LiaisonName { get; set; }//联系人姓名
        public string LiaisonIdNumber { get; set; }//联系人身份证号
        public string LegalIdNumber { get; set; }//法定代表人证件号
        public string CellPhoneNumber { get; set; }//联系人手机号码
        //public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
