using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Data;
using 拍卖系统.Models;

namespace 拍卖系统.Controllers
{
	[Produces("application/json")]
	[Route("api/AuctionRecords")]
	public class AuctionRecordsController : ControllerBase
	{
		public AuctionRecordsController(ApplicationDbContext context) : base(context) { }

		// GET: api/AuctionRecords
		[HttpGet("{id}")]
		public IEnumerable<AuctionRecord> GetAuctionRecords(int id)
		{
			return db.AuctionRecords.Where(a => a.Gid == id).OrderByDescending(a => a.Id);
		}

		// POST: api/AuctionRecords
		[HttpPost]
		public async Task<IActionResult> PostAuctionRecord([FromBody] AuctionRecord auctionRecord)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var auction = await db.Auctions.SingleOrDefaultAsync(a => a.Id == auctionRecord.Gid);
			if (auction == null)
				return NotFound();
			if (auction.StartTime > DateTime.Now)
				ThrowHttpResponseException("拍卖未开始");
			if (auction.EndTime < DateTime.Now)
				ThrowHttpResponseException("拍卖已结束");
			if (auction.NowPrice >= auctionRecord.Money)
				ThrowHttpResponseException("已经有人出价比你高，请重新出价");

			auction.EndStatus = EndStatus.进行中;
			auction.BidCount += 1;
			auction.Mid = auctionRecord.Mid;
			auction.NowPrice = auctionRecord.Money;

			db.AuctionRecords.Add(auctionRecord);
			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (AuctionRecordExists(auctionRecord.Id))
				{
					return new StatusCodeResult(StatusCodes.Status409Conflict);
				}
				else
				{
					throw;
				}
			}

			return Ok();
		}

		private bool AuctionRecordExists(int id)
		{
			return db.AuctionRecords.Any(e => e.Id == id);
		}
	}
}