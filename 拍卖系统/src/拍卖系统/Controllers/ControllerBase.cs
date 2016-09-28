using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Data;
using Microsoft.Extensions.Configuration;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Senparc.Weixin.MP.Containers;

namespace 拍卖系统.Controllers
{
	public class ControllerBase : Controller
	{
		protected ApplicationDbContext db;
		private readonly IConfiguration _config;
		public string AppId;
		public string AppSecret;
		public static string AccessToken;
		public string JsapiTicket;
		public string Token;//对应微信后台设置的Token，建议设置地复杂一些
		protected string EncodingAESKey;
		public OAuthAccessTokenResult OAuth
		{
			get
			{
				if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("OAuth")))
					return null;
				else
					return JsonConvert.DeserializeObject<OAuthAccessTokenResult>(HttpContext.Session.GetString("OAuth"));
			}
			set
			{
				HttpContext.Session.SetString("OAuth", JsonConvert.SerializeObject(value));
			}
		}
		public ControllerBase(ApplicationDbContext context)
		{
			db = context;
			if (_config == null)
				_config = Startup.Configuration;
			AppId = _config["AppId"];
			AppSecret = _config["AppSecret"];
			Token = _config["Token"];
			EncodingAESKey = _config["EncodingAESKey"];
			AccessToken = AccessTokenContainer.TryGetAccessToken(AppId, AppSecret);
			//获取JS票据
			JsapiTicket = JsApiTicketContainer.TryGetJsApiTicket(AppId, AppSecret);
		}
		/// <summary>
		/// 全局提示信息方法
		/// </summary>
		/// <param name="msg"></param>
		public void ShowCommMessage(string msg)
		{
			ViewData["CommMessage"] = msg;
		}
		/// <summary>
		/// 全局错误提示方法
		/// </summary>
		/// <param name="msg"></param>
		public void ShowErrMessage(string msg)
		{
			ViewData["ErrMessage"] = msg;
		}
		/// <summary>
		/// 抛出一个JSON异常，对应js error callback
		/// </summary>
		/// <param name="msg">错误信息</param>
		protected void ThrowHttpResponseException(string msg)
		{
			throw new JsonException(msg);
		}
		protected override void Dispose(bool disposing)
		{
			if (db != null)
				db.Dispose();

			base.Dispose(disposing);
		}
	}
}