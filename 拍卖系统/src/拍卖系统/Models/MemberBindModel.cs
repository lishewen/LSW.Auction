using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
    public class MemberBindModel
    {
		[Display(Name = "手机号")]
		[Required]
		public string Phone { get; set; }
		public string OpenId { get; set; }
	}
}
