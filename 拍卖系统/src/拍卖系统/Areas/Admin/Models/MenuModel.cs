using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Models;

namespace 拍卖系统.Areas.Admin.Models
{
	public class MenuModel : EntityBase<int>
	{
		public string GroupIDX { get; set; }
		public string GroupName { get; set; }
		public string GroupICO { get; set; }
		public IEnumerable<Groupitem> GroupItems { get; set; }
	}

	public class Groupitem : EntityBase<int>
	{
		public string ItemIDX { get; set; }
		public string ItemName { get; set; }
		public string ItemDesc { get; set; }
		public string ItemURL { get; set; }
		public string ItemICO { get; set; }
		public bool IsActive { get; set; }
	}
}
