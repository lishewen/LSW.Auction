using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Data;
using 拍卖系统.Controllers.Attributes;
using Microsoft.EntityFrameworkCore;

namespace 拍卖系统.Controllers
{
	public class MemberController : ControllerBase
	{
		public MemberController(ApplicationDbContext context) : base(context) { }
		[WeixinAuth]
		public async Task<IActionResult> Index()
		{
			var member = await db.Members.SingleOrDefaultAsync(m => m.OpenId == OAuth.openid);

			//拍卖列表
			ViewData["List"] = db.Auctions.Include(a => a.Good).OrderByDescending(a => a.Id).Take(6);

			return View(member);
		}
	}
}