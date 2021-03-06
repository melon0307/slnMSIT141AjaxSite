using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjMSIT141AjaxSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            //System.Threading.Thread.Sleep(5000); // 程式停止5秒
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
            // 檔案上傳要有實際路徑
            // C:\Users\Student\source\repos\slnMSIT141AjaxSite\prjMSIT141AjaxSite\wwwroot\uploads
            // string path = _host.ContentRootPath; // 會取得專案資料夾的實際路徑

            string path = Path.Combine(_host.WebRootPath, "uploads", file.FileName); // 會取得專案資料夾下wwwroot的實際路徑
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream); // 儲存檔案到uploads中
            }

            // 將檔案寫進資料庫
            byte[] imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imgByte;

            _context.Members.Add(member);
            _context.SaveChanges();

                string info = $"{file.FileName} - {file.ContentType} - {file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult City()
        {
            var city = _context.Addresses.Select(c => c.City).Distinct();
            return Json(city);
        }

        public IActionResult District(string city)
        {
            var district = _context.Addresses.Where(c => c.City == city).Select(d => d.SiteId).Distinct();
            return Json(district);
        }

        public IActionResult Roads(string district)
        {
            var roads = _context.Addresses.Where(d => d.SiteId == district).Select(r => r.Road);
            return Json(roads);
        }

        public IActionResult GetImageBytes(int id=1)
        {
            Member member = _context.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }
    }
}
