using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using credit.Models;

namespace credit.Controllers
{
    
    public class BaseController : Controller
    {
        [FromServices]
        public CreditContext DB { get; set; }
        // GET: /<controller>/

    }
}
