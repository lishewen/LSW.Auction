using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Data;
using 拍卖系统.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using 拍卖系统.Areas.Admin.Controllers.Helpers;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class GoodsController : ControllerBase
	{
		IHostingEnvironment _hostingEnvironment;
		public GoodsController(ApplicationDbContext context, IHostingEnvironment host) : base(context)
		{
			_hostingEnvironment = host;
		}

		// GET: Goods
		public async Task<IActionResult> Index()
		{
			return View(await db.Goods.ToListAsync());
		}

		// GET: Goods/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var good = await db.Goods.SingleOrDefaultAsync(m => m.Id == id);
			if (good == null)
			{
				return NotFound();
			}

			return View(good);
		}

		// GET: Goods/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Goods/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Good good)
		{
			if (ModelState.IsValid)
			{
				if (good.picdata != null)
				{
					var extName = ContentDispositionHeaderValue.Parse(good.picdata.ContentDisposition).FileName.Trim('"');
					int i = extName.LastIndexOf('.');
					extName = extName.Substring(i);
					string fileName = Guid.NewGuid() + extName;
					var filePath = _hostingEnvironment.WebRootPath + @"\upload\" + fileName;
					await good.picdata.SaveAsAsync(filePath);
					good.Picture = $"/upload/{fileName}";
				}

				good.UserId = User.Identity.Name;
				db.Add(good);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(good);
		}

		// GET: Goods/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var good = await db.Goods.SingleOrDefaultAsync(m => m.Id == id);
			if (good == null)
			{
				return NotFound();
			}
			return View(good);
		}

		// POST: Goods/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Good good)
		{
			if (id != good.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (good.picdata != null)
					{
						var extName = ContentDispositionHeaderValue.Parse(good.picdata.ContentDisposition).FileName.Trim('"');
						int i = extName.LastIndexOf('.');
						extName = extName.Substring(i);
						string fileName = Guid.NewGuid() + extName;
						var filePath = _hostingEnvironment.WebRootPath + @"\upload\" + fileName;
						await good.picdata.SaveAsAsync(filePath);
						good.Picture = $"/upload/{fileName}";
					}

					good.UserId = User.Identity.Name;
					good.UpdateTime = DateTime.Now;
					db.Update(good);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GoodExists(good.Id))
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
			return View(good);
		}

		// GET: Goods/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var good = await db.Goods.SingleOrDefaultAsync(m => m.Id == id);
			if (good == null)
			{
				return NotFound();
			}

			return View(good);
		}

		// POST: Goods/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var good = await db.Goods.SingleOrDefaultAsync(m => m.Id == id);
			db.Goods.Remove(good);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool GoodExists(int id)
		{
			return db.Goods.Any(e => e.Id == id);
		}
	}
}
