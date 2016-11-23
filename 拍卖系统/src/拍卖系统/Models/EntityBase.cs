using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
	/// <summary>
	/// 实体基类
	/// </summary>
	/// <typeparam name="TKey">主键类型</typeparam>
	public abstract class EntityBase<TKey>
	{
		/// <summary>
		/// 主键Id (主键类型根据继承时确定)
		/// </summary>
		[Key]
		public TKey Id { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		[Display(Name = "名称")]
		[Required(ErrorMessage = "不能为空")]
		public string Name { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; } = DateTime.Now;
		/// <summary>
		/// 是否删除  （逻辑删除而非物理删除）
		/// </summary>
		public bool IsDelete { get; set; } = false;
	}
}
