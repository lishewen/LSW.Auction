using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Areas.Admin.Controllers.Attributes
{
	/// <summary>
	/// 菜单Key
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class MenuKeyAttribute : ActionFilterAttribute
	{
		public string Key { get; set; }
		public MenuKeyAttribute(string key)
		{
			Key = key;
		}
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var c = context.Controller as Controller;
			c.ViewData["MenuKey"] = Key;
			base.OnActionExecuting(context);
		}
	}
}
