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
	public class AuctionController : ControllerBase
	{
		public AuctionController(ApplicationDbContext context) : base(context) { }
		[WeixinAuth]
		public async Task<IActionResult> Index(int id)
		{
			var member = await db.Members.SingleOrDefaultAsync(m => m.OpenId == OAuth.openid);
			if (member == null)
				return RedirectToAction("BindMemberCard", "Weixin");

			var auction = await db.Auctions.Include(a => a.Good).SingleOrDefaultAsync(a => a.Id == id);
			if (auction == null)
				return NotFound();

			return View(auction);
		}
	}
}