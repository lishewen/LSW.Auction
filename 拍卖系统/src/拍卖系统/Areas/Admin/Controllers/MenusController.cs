using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Areas.Admin.Models;
using 拍卖系统.Data;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class MenusController : ControllerBase
	{
		public MenusController(ApplicationDbContext context) : base(context) { }

		// GET: Menus
		public async Task<IActionResult> Index()
		{
			return View(await db.Menus.ToListAsync());
		}

		// GET: Menus/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var menuModel = await db.Menus.Include(m => m.MenuItems).SingleOrDefaultAsync(m => m.Id == id);
			if (menuModel == null)
			{
				return NotFound();
			}
			ViewData["GroupId"] = menuModel.Id;
			return View(menuModel.MenuItems);
		}

		// GET: Menus/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Menus/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Menu menuModel)
		{
			if (ModelState.IsValid)
			{
				menuModel.GroupName = menuModel.Name;
				menuModel.GroupIDX = Guid.NewGuid().ToString();
				db.Add(menuModel);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(menuModel);
		}

		public IActionResult CreateItem(int groupid)
		{
			ViewData["GroupId"] = groupid;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateItem(int groupid, Menuitem model)
		{
			var menuModel = await db.Menus.Include(m => m.MenuItems).SingleOrDefaultAsync(m => m.Id == groupid);
			if (menuModel == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				model.ItemName = model.Name;
				model.ItemIDX = Guid.NewGuid().ToString();
				menuModel.MenuItems.Add(model);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		// GET: Menus/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var menuModel = await db.Menus.SingleOrDefaultAsync(m => m.Id == id);
			if (menuModel == null)
			{
				return NotFound();
			}
			return View(menuModel);
		}

		// POST: Menus/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Menu menuModel)
		{
			if (id != menuModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					menuModel.GroupName = menuModel.Name;
					db.Update(menuModel);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MenuModelExists(menuModel.Id))
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
			return View(menuModel);
		}

		public async Task<IActionResult> EditItem(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var menuModel = await db.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
			if (menuModel == null)
			{
				return NotFound();
			}
			return View(menuModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditItem(int id, Menuitem model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					model.ItemName = model.Name;
					db.Update(model);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MenuModelExists(model.Id))
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
			return View(model);
		}

		// GET: Menus/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var menuModel = await db.Menus.SingleOrDefaultAsync(m => m.Id == id);
			if (menuModel == null)
			{
				return NotFound();
			}

			return View(menuModel);
		}

		// POST: Menus/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var menuModel = await db.Menus.SingleOrDefaultAsync(m => m.Id == id);
			db.Menus.Remove(menuModel);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> DeleteItem(int id)
		{
			var menuModel = await db.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
			db.MenuItems.Remove(menuModel);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool MenuModelExists(int id)
		{
			return db.Menus.Any(e => e.Id == id);
		}
	}
}
