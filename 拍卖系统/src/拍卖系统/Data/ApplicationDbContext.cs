using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Models;
using 拍卖系统.Areas.Admin.Models;

namespace 拍卖系统.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
		/// <summary>
		/// 商品表
		/// </summary>
		public DbSet<Good> Goods { get; set; }
		/// <summary>
		/// 会员表
		/// </summary>
		public DbSet<Member> Members { get; set; }
		/// <summary>
		/// 拍卖表
		/// </summary>
		public DbSet<Auction> Auctions { get; set; }
		/// <summary>
		/// 拍卖出价记录表
		/// </summary>
		public DbSet<AuctionRecord> AuctionRecords { get; set; }
		/// <summary>
		/// 一级菜单表
		/// </summary>
		public DbSet<Menu> Menus { get; set; }
		/// <summary>
		/// 二级菜单表
		/// </summary>
		public DbSet<Menuitem> MenuItems { get; set; }
	}
}
