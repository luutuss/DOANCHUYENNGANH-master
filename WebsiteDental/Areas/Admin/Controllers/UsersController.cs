﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteDental.Models;

namespace WebsiteDental.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UsersController : Controller
	{
		private readonly WebsiteDentalContext _context;

		public UsersController(WebsiteDentalContext context)
		{
			_context = context;
		}

		// GET: Admin/Users
		public async Task<IActionResult> Index()
		{
			var websiteDentalContext = _context.Users.Include(u => u.Role);
			return View(await websiteDentalContext.ToListAsync());
		}

		// GET: Admin/Users/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		// GET: Admin/Users/Create
		public IActionResult Create()
		{
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
			return View();
		}

		// POST: Admin/Users/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Username,Password,Email,Phone,RoleId,Avatar,CreatedAt,UpdatedAt,IsActive,Address")] User user)
		{
			if (ModelState.IsValid)
			{
				_context.Add(user);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
			return View(user);
		}

		// GET: Admin/Users/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
			return View(user);
		}

		// POST: Admin/Users/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email,Phone,RoleId,Avatar,CreatedAt,UpdatedAt,IsActive,Address")] User user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(user);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserExists(user.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
			return View(user);
		}

		// GET: Admin/Users/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		// POST: Admin/Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user != null)
			{
				_context.Users.Remove(user);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserExists(int id)
		{
			return _context.Users.Any(e => e.Id == id);
		}
	}
}