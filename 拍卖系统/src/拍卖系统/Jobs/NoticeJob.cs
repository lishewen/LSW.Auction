using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using 拍卖系统.Data;
using 拍卖系统.Services;

namespace 拍卖系统.Jobs
{
	public class NoticeJob : Job
	{
		ApplicationDbContext context;
		IWeixinSender weixinsender;
		public NoticeJob(ApplicationDbContext context, IWeixinSender weixinsender)
		{
			this.context = context;
			this.weixinsender = weixinsender;
		}

		public void Start()
		{
			SendALL("有一项拍卖即将开始，请登录查看");
			context.TimedJobs.Where(j => j.Id == "拍卖系统.Jobs.NoticeJob.Start").Delete();
		}

		public void End()
		{
			SendALL("有一项拍卖即将结束，请登录查看");
			context.TimedJobs.Where(j => j.Id == "拍卖系统.Jobs.NoticeJob.End").Delete();
		}

		private void SendALL(string msg)
		{
			foreach(var m in context.Members)
			{
				weixinsender.SendWeixinAsync(m.OpenId, msg);
			}
		}
	}
}
