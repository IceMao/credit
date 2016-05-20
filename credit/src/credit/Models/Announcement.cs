using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class Announcement
    {
        //公告
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublicUnit { get; set; }
        public DateTime PublicTime { get; set; }
        public string Content { get; set; }

        public string Writer { get; set; }
        public DateTime WriteTime { get; set; } 
        

        //公告类型
        [ForeignKey("TypeCS")]
        public int TypeId { get; set; }//R /U /I
        public virtual TypeCS TypeCS { get; set; }
    }
}
