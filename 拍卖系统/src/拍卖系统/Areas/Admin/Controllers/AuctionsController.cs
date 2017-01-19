using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Data;
using 拍卖系统.Models;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using 拍卖系统.Services;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class AuctionsController : ControllerBase
	{
		IWeixinSender weixinsender;
		public AuctionsController(ApplicationDbContext context, IWeixinSender weixinsender) : base(context)
		{
			this.weixinsender = weixinsender;
		}

		// GET: Auctions
		public async Task<IActionResult> Index()
		{
			return View(await db.Auctions.ToListAsync());
		}

		public IActionResult Center()
		{
			return View();
		}

		// GET: Auctions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var auction = await db.Auctions.SingleOrDefaultAsync(m => m.Id == id);
			if (auction == null)
			{
				return NotFound();
			}

			return View(auction);
		}

		// GET: Auctions/Create
		public async Task<IActionResult> Create(int gid)
		{
			var good = await db.Goods.FirstOrDefaultAsync(g => g.Id == gid);
			return View(new Auction { Gid = gid, Good = good, Name = good.Name });
		}

		// POST: Auctions/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Auction auction)
		{
			if (ModelState.IsValid)
			{
				db.Add(auction);

				var beginTime = auction.StartTime.AddMinutes(-15);
				var endTime = auction.EndTime.AddMinutes(-15);

				RecurringJob.AddOrUpdate($"开始通知——{auction.Name}", () => SendStartMsg(auction.Name), Cron.Yearly(beginTime.Month, beginTime.Day, beginTime.Hour, beginTime.Minute), TimeZoneInfo.Local);
				RecurringJob.AddOrUpdate($"结束通知——{auction.Name}", () => SendEndMsg(auction.Name), Cron.Yearly(endTime.Month, endTime.Day, endTime.Hour, endTime.Minute), TimeZoneInfo.Local);
				
				await db.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			return View(auction);
		}

		// GET: Auctions/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var auction = await db.Auctions.SingleOrDefaultAsync(m => m.Id == id);
			if (auction == null)
			{
				return NotFound();
			}
			return View(auction);
		}

		// POST: Auctions/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Auction auction)
		{
			if (id != auction.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					db.Update(auction);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AuctionExists(auction.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index");
			}
			return View(auction);
		}

		// GET: Auctions/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var auction = await db.Auctions.SingleOrDefaultAsync(m => m.Id == id);
			if (auction == null)
			{
				return NotFound();
			}

			return View(auction);
		}

		// POST: Auctions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var auction = await db.Auctions.SingleOrDefaultAsync(m => m.Id == id);
			db.Auctions.Remove(auction);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[Produces("application/json")]
		[Route("api/Auctions")]
		[HttpGet]
		public IEnumerable<Auction> GetAuctions()
		{
			return db.Auctions.Include(a => a.AuctionRecords)
				.ThenInclude(a => a.Member)
				.Where(a => a.StartTime <= DateTime.Now && a.EndTime >= DateTime.Now);
		}

		private bool AuctionExists(int id)
		{
			return db.Auctions.Any(e => e.Id == id);
		}

		private void SendALL(string msg)
		{
			foreach (var m in db.Members)
			{
				weixinsender.SendWeixinAsync(m.OpenId, msg);
			}
		}
		[NonAction]
		public void SendStartMsg(string name)
		{
			SendALL($"{name}拍卖即将开始");
			RecurringJob.RemoveIfExists($"开始通知——{name}");
		}
		[NonAction]
		public void SendEndMsg(string name)
		{
			SendALL($"{name}拍卖即将结束");
			RecurringJob.RemoveIfExists($"结束通知——{name}");
		}
	}
}
