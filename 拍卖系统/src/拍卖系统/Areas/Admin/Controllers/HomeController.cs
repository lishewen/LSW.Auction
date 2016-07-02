using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Data;

namespace 拍卖系统.Areas.Admin.Controllers
{
    public class HomeController : ControllerBase
    {
		public HomeController(ApplicationDbContext context) : base(context) { }
		public IActionResult Index()
        {
            return View();
        }
    }
}