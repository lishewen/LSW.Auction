using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	public class RoleViewModel
	{
		public string Id { get; set; }
		[Required(AllowEmptyStrings = false)]
		[Display(Name = "角色名称")]
		public string Name { get; set; }
	}

	public class EditUserViewModel
	{
		public string Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "电邮地址")]
		//[EmailAddress]
		public string Email { get; set; }

		public IEnumerable<SelectListItem> RolesList { get; set; }
	}
}
