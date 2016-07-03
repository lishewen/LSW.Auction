using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Models;

namespace 拍卖系统.Areas.Admin.Models
{
	public class Menu : EntityBase<int>
	{
		public Menu()
		{
			if (MenuItems == null)
				MenuItems = new List<Menuitem>();
		}
		public string GroupIDX { get; set; }
		[Display(Name = "组名")]
		public string GroupName { get; set; }
		[Display(Name = "图标")]
		public string GroupICO { get; set; }
		public virtual ICollection<Menuitem> MenuItems { get; set; }
		[Display(Name = "控制器名")]
		public string ControllerName { get; set; }
	}

	public class Menuitem : EntityBase<int>
	{
		public int MenuId { get; set; }
		[ForeignKey("MenuId")]
		public virtual Menu Menu { get; set; }
		public string ItemIDX { get; set; }
		[Display(Name = "菜单名")]
		public string ItemName { get; set; }
		[Display(Name = "排序")]
		public int ItemDesc { get; set; }
		public string ItemURL { get; set; }
		[Display(Name = "图标")]
		public string ItemICO { get; set; }
		[Display(Name = "动作名")]
		public string ActionName { get; set; }
	}
}
