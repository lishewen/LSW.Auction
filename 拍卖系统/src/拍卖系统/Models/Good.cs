using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[Display(Name = "商品描述")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		/// <summary>
		/// 商品价格
		/// </summary>
		[Display(Name = "商品价格")]
		public decimal Price { get; set; }
		/// <summary>
		/// 商品详情
		/// </summary>
		[Display(Name = "内容")]
		public string Content { get; set; }
		/// <summary>
		/// 商品图片
		/// </summary>
		[Display(Name = "首图")]
		public string Picture { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 发布者
		/// </summary>
		[Display(Name = "发布者")]
		public string UserId { get; set; }

	}
}
