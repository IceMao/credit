using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public enum Result
    {
        开业,
        歇业,
        清算,
        迁出
    }
    public class InfoRandom
    {
        //抽查信息公示
        public int Id { get; set; }
        [StringLength(12)]
        public string RegistrationNumber { get; set; }
        [StringLength(12, ErrorMessage = "注册号长度不对，请检查")]
        public DateTime DateTime { get; set; }
        public Result Result { get; set; } //抽查结果

        //注册号，企业名称外键中的信息？？？？怎么用？？
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
