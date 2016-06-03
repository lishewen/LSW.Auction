using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	/// <summary>
	/// 商品
	/// </summary>
	public class Good : EntityBase<int>
	{
		/// <summary>
		/// 商品描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 商品价格
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// 商品详情
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 商品图片
		/// </summary>
		public string Picture { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 发布者
		/// </summary>
		public string UserId { get; set; }

	}
}
