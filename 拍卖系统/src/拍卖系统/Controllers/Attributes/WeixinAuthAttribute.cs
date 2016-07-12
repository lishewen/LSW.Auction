using LSW.Weixin.MP;
using LSW.Weixin.MP.AdvancedAPIs;
using LSW.Weixin.MP.AdvancedAPIs.OAuth;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Controllers.Attributes
{
	/// <summary>
	/// 微信验证登录跳转
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class WeixinAuthAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var c = context.Controller as ControllerBase;
#if (!DEBUG)
			if (c.OAuth == null)
			{
				var state = c.Request.Query["state"];
				if (state != c.Token)
					context.Result = c.Redirect(OAuthApi.GetAuthorizeUrl(c.AppId, "http://" + c.Request.Host + c.Request.Path, c.Token, OAuthScope.snsapi_base));
				else
				{
					var code = c.Request.Query["code"];
					c.OAuth = OAuthApi.GetAccessToken(c.AppId, c.AppSecret, code);
				}
			}
#else
			c.OAuth = new OAuthAccessTokenResult { openid = "o3vTkwWREjtcPiHDYTT78SHvPHmY" };
#endif
			base.OnActionExecuting(context);
		}
	}
}
