﻿using Microsoft.AspNetCore.Mvc;
using prjMSIT141AjaxSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjMSIT141AjaxSite.Controllers
{
    public class ApiController : Controller
    {
        private DemoContext _context;
        public ApiController(DemoContext context)
        {
            _context = context;
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

        public IActionResult Register(Member member)
        {
            var mem = _context.Members.FirstOrDefault(m => m.Name == member.Name);
            if (mem == null)
                return Content("True");   
            
            return Content("False");
        }
    }
}
