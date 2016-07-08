using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole() : base() { }
		public ApplicationRole(string name) : base(name) { }
	}
}
