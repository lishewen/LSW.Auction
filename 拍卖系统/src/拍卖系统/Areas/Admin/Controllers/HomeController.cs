using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using 拍卖系统.Areas.Admin.Controllers.Helpers;
using 拍卖系统.Data;

namespace 拍卖系统.Areas.Admin.Controllers
{
    public class HomeController : ControllerBase
    {
        IHostingEnvironment _hostingEnvironment;
        public HomeController(ApplicationDbContext context, IHostingEnvironment host) : base(context)
        {
            _hostingEnvironment = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files[0];
            var originalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            int i = originalName.LastIndexOf('.');
            string extName = originalName.Substring(i);
            string fileName = Guid.NewGuid() + extName;
            var filePath = _hostingEnvironment.WebRootPath + @"\upload\" + fileName;
            await file.SaveAsAsync(filePath);

            string callback = Request.Query["callback"];

            Hashtable infoList = new Hashtable();

            infoList.Add("state", "SUCCESS");
            infoList.Add("url", $"/upload/{fileName}");
            infoList.Add("originalName", originalName);
            infoList.Add("name", fileName);
            infoList.Add("size", file.Length);
            infoList.Add("type", extName);

            string json = BuildJson(infoList);

            if (callback != null)
                return Content(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json), "text/html");
            else
                return Content(json, "text/html");
        }

        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "originalName", "name", "url", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }
    }
}