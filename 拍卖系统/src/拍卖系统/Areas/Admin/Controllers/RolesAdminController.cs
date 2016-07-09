using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Data;
using Microsoft.AspNetCore.Identity;
using 拍卖系统.Models;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class RolesAdminController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		public RolesAdminController(ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager) : base(context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View(_roleManager.Roles);
		}

		//异步读取角色详情
		// GET: /Roles/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return new BadRequestResult();
			}
			var role = await _roleManager.FindByIdAsync(id);
			// 读取角色内的用户列表。
			var users = new List<ApplicationUser>();
			foreach (var user in _userManager.Users.ToList())
			{
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					users.Add(user);
				}
			}
			ViewBag.Users = users;
			ViewBag.UserCount = users.Count();
			return View(role);
		}

		//读取角色创建
		// GET: /Roles/Create
		public ActionResult Create()
		{
			return View();
		}

		//异步写入角色创建
		// POST: /Roles/Create
		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel roleViewModel)
		{
			if (ModelState.IsValid)
			{
				var role = new ApplicationRole(roleViewModel.Name);
				var roleresult = await _roleManager.CreateAsync(role);
				if (!roleresult.Succeeded)
				{
					ModelState.AddModelError("", roleresult.Errors.First().Description);
					return View();
				}
				return RedirectToAction("Index");
			}
			return View();
		}

		//异步读取角色编辑
		// GET: /Roles/Edit/Admin
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return new BadRequestResult();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}
			RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
			return View(roleModel);
		}

		//异步写入角色编辑
		// POST: /Roles/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RoleViewModel roleModel)
		{
			if (ModelState.IsValid)
			{
				var role = await _roleManager.FindByIdAsync(roleModel.Id);
				role.Name = roleModel.Name;
				await _roleManager.UpdateAsync(role);
				return RedirectToAction("Index");
			}
			return View();
		}

		//
		//异步读取角色删除
		// GET: /Roles/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return new BadRequestResult();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}
			return View(role);
		}

		//异步写入角色删除
		// POST: /Roles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id, string deleteUser)
		{
			if (ModelState.IsValid)
			{
				if (id == null)
				{
					return new BadRequestResult();
				}
				var role = await _roleManager.FindByIdAsync(id);
				if (role == null)
				{
					return NotFound();
				}
				IdentityResult result;
				if (deleteUser != null)
				{
					result = await _roleManager.DeleteAsync(role);
				}
				else
				{
					result = await _roleManager.DeleteAsync(role);
				}
				if (!result.Succeeded)
				{
					ModelState.AddModelError("", result.Errors.First().Description);
					return View();
				}
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}