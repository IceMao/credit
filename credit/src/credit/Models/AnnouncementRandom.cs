using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class AnnouncementRandom
    {
        //信息公告——抽查检查信息公告
        public int Id { get; set; }
        //1.管理员录入公告
        public string title { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        //2.外部导入公告???????
    }
}
