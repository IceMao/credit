using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class TypeCS
    {
        public int Id { get; set; }
        public string NameType { get; set; }//EType(企业经营状态)，PType（抽查公示类别）
        public string Types { get; set; }
        public string descript { get; set; }
    }
}
