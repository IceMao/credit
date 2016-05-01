using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public class YearReportIndividual
    {
        //个体年度报告
        public int Id { get; set; }
        //行政许可取得和变动信息
        //生产经营信息
        //开设的网站或从事网络经营的网店的名称，网址等信息
        //联系方式等信息
        //国家工商行政管理总局要求报送的其他信息
        //


        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
