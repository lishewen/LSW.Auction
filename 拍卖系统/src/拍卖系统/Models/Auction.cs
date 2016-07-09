using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	/// <summary>
	/// 拍卖
	/// </summary>
	public class Auction : EntityBase<int>
	{
		/// <summary>
		/// 商品id
		/// </summary>
		public int Gid { get; set; }
		[ForeignKey("Gid")]
		public virtual Good Good { get; set; }
		/// <summary>
		/// 拍品编号
		/// </summary>
		[Display(Name = "拍品编号")]
		public string Bidnb { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Display(Name = "状态")]
		public bool Status { get; set; }
		/// <summary>
		/// 起拍价
		/// </summary>
		[Display(Name = "起拍价")]
		public decimal Onset { get; set; }
		/// <summary>
		/// 保留价
		/// </summary>
		[Display(Name = "保留价")]
		public decimal Price { get; set; }
		/// <summary>
		/// 当前价
		/// </summary>
		public decimal NowPrice { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		[Display(Name = "开始时间")]
		public DateTime StartTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 结束时间
		/// </summary>
		[Display(Name = "结束时间")]
		public DateTime EndTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 价格浮动
		/// </summary>
		[Display(Name = "价格浮动")]
		public decimal StepSize { get; set; }
		/// <summary>
		/// 当前出价人id
		/// </summary>
		public int Mid { get; set; }
		/// <summary>
		/// 出价次数
		/// </summary>
		public int BidCount { get; set; }
		/// <summary>
		/// 结束状态
		/// </summary>
		public EndStatus EndStatus { get; set; }
	}
	public enum EndStatus
	{
		未开始,
		进行中,
		成交,
		流拍
	}
}
