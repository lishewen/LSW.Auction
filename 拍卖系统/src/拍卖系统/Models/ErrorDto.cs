using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
    public class ErrorDto
    {
		public int Code { get; set; }
		public string Message { get; set; }

		// other fields

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
