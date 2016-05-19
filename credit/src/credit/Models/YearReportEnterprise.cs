using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class YearReportEnterprise
    {
        //企业年度报告
        public DateTime WriteTime { get; set; }

        //个体年度报告
        public int Id { get; set; }
        //填报年度
        public int DateTime { get; set; }
        //企业联系电话
        public string Tel { get; set; }
        //企业通信地址
        public string Address { get; set; }
        //邮政编码
        public string ZipCode { get; set; }
        //电子邮箱
        public string Email { get; set; }
        //类别
        public string OperatState { get; set; }
        //企业是否有投资信息或购买其他公司股权（是否）
        public string Investment { get; set; }
        //是否有网站或网点
        public string Website { get; set; }
        //从业人数 （应填写当但可以选择公示/不公示）
        public int EmployeeNum { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
