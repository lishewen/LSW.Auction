using Senparc.Weixin.MP.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Controllers.Attributes
{
	/// <summary>
	/// 需要引用JSSDK页的方法
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class JSSDKAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			var c = context.Controller as ControllerBase;
			//获取时间戳
			var timestamp = JSSDKHelper.GetTimestamp();
			//获取随机码
			var nonceStr = JSSDKHelper.GetNoncestr();
			//获取签名
			var signature = JSSDKHelper.GetSignature(c.JsapiTicket, nonceStr, timestamp, "http://" + c.Request.Host + c.Request.Path + c.Request.QueryString);


			c.ViewData["AppId"] = c.AppId;
			c.ViewData["Timestamp"] = timestamp;
			c.ViewData["NonceStr"] = nonceStr;
			c.ViewData["Signature"] = signature;

			base.OnActionExecuted(context);
		}
	}
}
