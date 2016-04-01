﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class InfoIllegal
    {
        //严重违法信息公示
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string EnterpriseName { get; set; }
        public DateTime DateTime { get; set; }
        
    }
}
