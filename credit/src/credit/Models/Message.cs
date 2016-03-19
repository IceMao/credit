using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public enum State
    {
        正常,
        严重违法,
        经营异常
    }
    public class Message
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }//报告年度
        public string EnterpriseName { get; set; }//企业名称
        public string EnterpriseTel { get; set; }
        public string EnterpriseAddress { get; set; }
        public string EnterpriseEmail { get; set; }
        public string ZipCode { get; set; }


        public State State { get; set; }//状态

        [ForeignKey("User")]
        public string RegistrationNumber { get; set; }//注册号“获取”
        public virtual User User { get; set; }
    }
}
