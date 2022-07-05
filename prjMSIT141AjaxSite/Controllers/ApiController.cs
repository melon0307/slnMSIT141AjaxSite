using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjMSIT141AjaxSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjMSIT141AjaxSite.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext context , IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index(CUser user)
        {
            //System.Threading.Thread.Sleep(5000); //程式停止5秒
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}你好, 你的年齡是{user.age}。");
        }

        public IActionResult CheckAccount(string name)
        {
            var exist = _context.Members.Any(m => m.Name == name);
            return Content(exist.ToString(), "text/plain"); 
        }

        public IActionResult Register(Member member, IFormFile file)
        {

            string info = $"{file.FileName} - {file.ContentType} - {file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
