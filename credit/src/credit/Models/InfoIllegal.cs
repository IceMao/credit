using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class InfoIllegal
    {
        //严重违法信息
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime DateTime { get; set; }
        //其中注册号，企业名是外键中得到的 怎么用？
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
