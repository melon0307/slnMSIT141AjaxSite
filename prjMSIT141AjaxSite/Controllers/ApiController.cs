using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjMSIT141AjaxSite.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return Content("AJax, 你好","text/plain",System.Text.Encoding.UTF8);
        }
    }
}
