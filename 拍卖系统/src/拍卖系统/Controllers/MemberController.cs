using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Data;
using 拍卖系统.Controllers.Attributes;

namespace 拍卖系统.Controllers
{
	public class MemberController : ControllerBase
	{
		public MemberController(ApplicationDbContext context) : base(context) { }
		[WeixinAuth]
		public IActionResult Index()
		{

			return View();
		}
	}
}