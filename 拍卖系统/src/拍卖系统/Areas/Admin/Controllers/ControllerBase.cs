using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Data;

namespace 拍卖系统.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ControllerBase : Controller
	{
		protected ApplicationDbContext db;
		public ControllerBase(ApplicationDbContext context)
		{
			db = context;
		}
		protected override void Dispose(bool disposing)
		{
			if (db != null)
				db.Dispose();

			base.Dispose(disposing);
		}
		/// <summary>
		/// 全局提示信息方法
		/// </summary>
		/// <param name="msg"></param>
		public void ShowCommMessage(string msg)
		{
			ViewData["CommMessage"] = msg;
		}
		/// <summary>
		/// 全局错误提示方法
		/// </summary>
		/// <param name="msg"></param>
		public void ShowErrMessage(string msg)
		{
			ViewData["ErrMessage"] = msg;
		}
	}
}
