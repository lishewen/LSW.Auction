using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using 拍卖系统.Areas.Admin.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using 拍卖系统.Data;
using Microsoft.EntityFrameworkCore;

namespace 拍卖系统.Areas.Admin.Controllers.Helpers
{
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("adminmenu")]
	public class MenuTagHelper : TagHelper
	{
		[HtmlAttributeName("items")]
		public IEnumerable<Menu> Items { get; set; }
		[ViewContext]
		public ViewContext ViewContext { get; set; }
		public ApplicationDbContext db { get; set; }
		public MenuTagHelper(ApplicationDbContext context)
		{
			db = context;
		}
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (Items == null)
				Items = db.Menus.Include(m => m.MenuItems);

			output.TagName = "ul";
			output.Attributes.Add("class", "sidebar-menu");
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<li class=\"header\"><b>功能导航</b></li>");

			var routeData = ViewContext.RouteData.Values;
			var currentController = routeData["controller"];
			var currentAction = routeData["action"];

			foreach (var i in Items)
			{
				if (string.Equals(i.ControllerName, currentController as string, StringComparison.OrdinalIgnoreCase))
					sb.AppendLine("<li class=\"treeview active\">");
				else
					sb.AppendLine("<li class=\"treeview\">");
				sb.AppendLine("<a href=\"#\">");
				sb.AppendLine($"<i class=\"{i.GroupICO}\"></i>");
				sb.AppendLine($"<span>{i.GroupName}</span>");
				sb.AppendLine("<i class=\"fa fa-angle-left pull-right\"></i>");
				sb.AppendLine("</a>");
				sb.AppendLine("<ul class=\"treeview-menu\">");

				foreach (var g in i.MenuItems)
				{
					if (string.Equals(currentAction as string, g.ActionName, StringComparison.OrdinalIgnoreCase) &&
						string.Equals(currentController as string, g.Menu.ControllerName, StringComparison.OrdinalIgnoreCase))
						sb.AppendLine("<li class=\"active\">");
					else
						sb.AppendLine("<li>");
					sb.AppendLine($"<a href=\"{g.ItemURL}\"><i class=\"{g.ItemICO}\"></i>{g.ItemName}</a>");
					sb.AppendLine("</li>");
				}

				sb.AppendLine("</ul>");
				sb.AppendLine("</li>");
			}

			output.PostContent.SetHtmlContent(sb.ToString());
			output.TagMode = TagMode.StartTagAndEndTag;
		}
	}
}
