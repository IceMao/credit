using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class BaseInfo
    {
        //最基本的信息，由管理员添加，当联络员注册输入注册号时判断 BaseInfo中是否存在
        public int Id { get; set; }
        //[StringLength(12)]
        public string RegistrationNumber { get; set; }
        //[StringLength(12, ErrorMessage = "注册号长度不对，请检查")]
        public string EnterpriseName { get; set; }

    }
}
