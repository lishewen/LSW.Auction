using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 拍卖系统.Models;
using Microsoft.AspNetCore.Identity;
using 拍卖系统.Data;
using Microsoft.EntityFrameworkCore;
using 拍卖系统.Models.AccountViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace 拍卖系统.Areas.Admin.Controllers
{
	public class UsersAdminController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		public UsersAdminController(ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager) : base(context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		/// <summary>
		/// 获取用户组名字符串
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <returns></returns>
		public async Task<string> GetRoles(ApplicationUser userid)
		{
			var rlist = await _userManager.GetRolesAsync(userid);
			return string.Join(",", rlist.ToArray());
		}
		public async Task<IActionResult> Index()
		{
			var model = await _userManager.Users.ToListAsync();
			var dict = new Dictionary<string, string>();
			foreach (var u in model)
			{
				dict.Add(u.Id, await GetRoles(u));
			}
			ViewBag.Roles = dict;
			return View(model);
		}
		//异步读取用户详情
		//GET: /Users/Details/5
		public async Task<IActionResult> Details(string id)
		{
			//用户为空时返回400错误
			if (id == null)
			{
				return new BadRequestResult();
			}
			//按Id查找用户
			var user = await _userManager.FindByIdAsync(id);
			ViewBag.RoleNames = await _userManager.GetRolesAsync(user);
			return View(user);
		}
		//异步读取用户创建
		//GET:/Users/Create
		public async Task<IActionResult> Create()
		{
			//读取角色列表
			ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
			return View(new RegisterViewModel());
		}
		[HttpPost]
		public async Task<IActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = userViewModel.Email,
				};
				var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

				if (adminresult.Succeeded)
				{
					////只有Admin能修改组成员
					if (User.IsInRole("Admin"))
					{
						if (selectedRoles != null)
						{
							var result = await _userManager.AddToRolesAsync(user, selectedRoles);
							if (!result.Succeeded)
							{
								ModelState.AddModelError("", result.Errors.First().Description);
								ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
								return View();
							}
						}
					}
				}
				else
				{
					ModelState.AddModelError("", adminresult.Errors.First().Description);
					ShowErrMessage(adminresult.Errors.First().Description);
					ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
					return View();
				}
				return RedirectToAction("Index");
			}
			ShowErrMessage("创建用户失败");
			ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
			return View();
		}
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return new BadRequestResult();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var userRoles = await _userManager.GetRolesAsync(user);
			return View(new EditUserViewModel()
			{
				Id = user.Id,
				Email = user.UserName,
				RolesList = _roleManager.Roles.ToList().Select(x => new SelectListItem()
				{
					Selected = userRoles.Contains(x.Name),
					Text = x.Name,
					Value = x.Name
				})
			});
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditUserViewModel editUser, params string[] selectedRole)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(editUser.Id);
				if (user == null)
				{
					return NotFound();
				}
				user.UserName = editUser.Email;

				await db.SaveChangesAsync();
				//只有Admin能修改组成员
				if (User.IsInRole("Admin"))
				{
					var userRoles = await _userManager.GetRolesAsync(user);
					selectedRole = selectedRole ?? new string[] { };

					//从全部权限中撤销权限
					//if (userRoles.Count == RoleManager.Roles.Count())
					//{
					await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRole).ToArray<string>());
					//}

					var result = await _userManager.AddToRolesAsync(user, selectedRole.Except(userRoles).ToArray<string>());
					if (!result.Succeeded)
					{
						ModelState.AddModelError("", result.Errors.First().Description);
						return View();
					}
				}
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", "操作失败。");
			ShowErrMessage("操作失败。");
			return View();
		}
		//读取用户删除
		// GET: /Users/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return new BadRequestResult();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}
		//写入角色删除
		// POST: /Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			if (ModelState.IsValid)
			{
				if (id == null)
				{
					return new BadRequestResult();
				}
				var user = await _userManager.FindByIdAsync(id);
				if (user == null)
				{
					return NotFound();
				}
				var result = await _userManager.DeleteAsync(user);
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