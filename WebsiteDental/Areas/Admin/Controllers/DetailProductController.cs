using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteDental.Models;

namespace WebsiteDental.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DetailProductController : Controller
	{
		private readonly WebsiteDentalContext _context;

		public DetailProductController(WebsiteDentalContext context)
		{
			_context = context;
		}		

		public async Task<IActionResult> Index()
		{
			var details = await _context.ProductDetails
				.Include(p => p.Product)
				.Include(p => p.Category)
				.ToListAsync();
			return View(details);
		}

		public IActionResult Create()
		{
			var products = _context.Products
		.Select(p => new SelectListItem
		{
			Value = p.Id.ToString(),
			Text = p.ProductName // tên sản phẩm muốn hiển thị
		})
		.ToList();

			ViewBag.ProductId = products;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductDetail detail)
		{
			if (ModelState.IsValid)
			{
				detail.CreatedAt = DateTime.Now;
				_context.ProductDetails.Add(detail);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(detail);
		}

		public IActionResult Edit(int id)
		{
			var detail = _context.ProductDetails.Find(id);
			if (detail == null) return NotFound();
			ViewBag.Products = _context.Products.ToList();
			ViewBag.Categories = _context.ProductCategories.ToList();
			return View(detail);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProductDetail detail)
		{
			if (ModelState.IsValid)
			{
				_context.ProductDetails.Update(detail);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(detail);
		}

		public IActionResult Delete(int id)
		{
			var detail = _context.ProductDetails.Find(id);
			if (detail == null) return NotFound();
			return View(detail);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var detail = await _context.ProductDetails.FindAsync(id);
			if (detail != null)
			{
				_context.ProductDetails.Remove(detail);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}
	}
}
