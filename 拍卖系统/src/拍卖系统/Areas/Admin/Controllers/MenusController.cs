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

            var menuModel = await db.Menus.SingleOrDefaultAsync(m => m.Id == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
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
        public async Task<IActionResult> Create([Bind("Id,ControllerName,CreateTime,GroupICO,GroupIDX,GroupName,IsDelete,Name")] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(menuModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ControllerName,CreateTime,GroupICO,GroupIDX,GroupName,IsDelete,Name")] MenuModel menuModel)
        {
            if (id != menuModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        private bool MenuModelExists(int id)
        {
            return db.Menus.Any(e => e.Id == id);
        }
    }
}
