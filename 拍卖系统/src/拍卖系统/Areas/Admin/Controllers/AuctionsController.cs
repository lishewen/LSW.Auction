using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Data;
using 拍卖系统.Models;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class AuctionsController : ControllerBase
	{
		public AuctionsController(ApplicationDbContext context) : base(context) { }

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
	}
}
