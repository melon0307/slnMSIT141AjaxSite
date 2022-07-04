using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjMSIT141AjaxSite.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string name, int age = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Ajax";
            }
            return Content($"{name}你好, 你的年齡是{age}。");
        }
    }
}
