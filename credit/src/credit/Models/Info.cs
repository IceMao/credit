using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class Info
    {
        public int Id { get; set; }
        public string RegisteNumber { get; set; }
        public string CompanyName { get; set; }

        public string InReason{get;set;}
        public string OutReason { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime WriteTime { get; set; }
        public string WriteName { get; set; }
        public string PublicUnit { get; set; }//检查机关
        public string Result { get; set; } //抽查结果

        //公示类型
        [ForeignKey("TypeCS")]
        public int TypeId { get; set; }//Rin /Uin /Iin  ----抽查公示结果类型：RType
        public virtual TypeCS TypeCS { get; set; }
    }
}
