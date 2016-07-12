using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace 拍卖系统.Models
{
	/// <summary>
	/// 拍卖出价记录
	/// </summary>
	public class AuctionRecord : EntityBase<int>
	{
		/// <summary>
		/// 拍品id
		/// </summary>
		public int Gid { get; set; }
		[ForeignKey("Gid")]
		[JsonIgnore]
		public virtual Auction Auction { get; set; }
		/// <summary>
		/// 用户id
		/// </summary>
		public int Mid { get; set; }
		[ForeignKey("Mid")]
		public virtual Member Member { get; set; }
		/// <summary>
		/// 出价金额
		/// </summary>
		public decimal Money { get; set; }
		/// <summary>
		/// 出价后
		/// </summary>
		public decimal Bided { get; set; }
		/// <summary>
		/// 出价方式
		/// </summary>
		public AuctionRecordType Type { get; set; }
	}
	public enum AuctionRecordType
	{
		手动,
		自动
	}
}
