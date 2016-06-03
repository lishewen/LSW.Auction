using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	/// <summary>
	/// 会员
	/// </summary>
	public class Member : EntityBase<int>
	{
		/// <summary>
		/// 用户组ID
		/// </summary>
		public int GroupId { get; set; }
		/// <summary>
		/// 会员等级ID
		/// </summary>
		public int LevelId { get; set; }
		/// <summary>
		/// 推荐人ID
		/// </summary>
		public int AgentId { get; set; }
		/// <summary>
		/// 微信openid
		/// </summary>
		public string OpenId { get; set; }
		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// 微信号
		/// </summary>
		public string Weixin { get; set; }
		/// <summary>
		/// 累计佣金
		/// </summary>
		public decimal Commission { get; set; }
		/// <summary>
		/// 打款佣金
		/// </summary>
		public decimal Commission_Pay { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 成为代理时间
		/// </summary>
		public DateTime AgentTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 状态
		/// </summary>
		public bool Status { get; set; } = true;
		/// <summary>
		/// 是否代理
		/// </summary>
		public bool IsAgent { get; set; } = false;
		/// <summary>
		/// 点击数
		/// </summary>
		public int ClickCount { get; set; }
		/// <summary>
		/// 分销商等级ID
		/// </summary>
		public int AgentLevelId { get; set; }
		public string NoticeSet { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		/// 积分1（整形）
		/// </summary>
		public int Credit1 { get; set; }
		/// <summary>
		/// 积分2（可小数）
		/// </summary>
		public decimal Credit2 { get; set; }
		/// <summary>
		/// 生日
		/// </summary>
		public DateTime Birthday { get; set; } = DateTime.Now;
		/// <summary>
		/// 性别
		/// </summary>
		public Gender Gender { get; set; } = Gender.未知;
		/// <summary>
		/// 头像
		/// </summary>
		public string Avatar { get; set; }
		/// <summary>
		/// 省
		/// </summary>
		public string Province { get; set; }
		/// <summary>
		/// 市
		/// </summary>
		public string City { get; set; }
		/// <summary>
		/// 区
		/// </summary>
		public string Area { get; set; }
	}
	public enum Gender
	{
		未知,
		男,
		女
	}
}
