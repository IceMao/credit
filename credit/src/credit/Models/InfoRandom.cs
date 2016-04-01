using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
   
    public class InfoRandom
    {
        //抽查信息公示
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string EnterpriseName { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; } //抽查结果
        
    }
}
