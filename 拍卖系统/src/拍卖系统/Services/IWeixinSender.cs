using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Services
{
	public interface IWeixinSender
	{
		Task SendWeixinAsync(string openid, string message);
	}
}
