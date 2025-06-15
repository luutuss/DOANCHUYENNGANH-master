using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteDental.Models;

namespace WebsiteDental.Controllers
{
    [Route("About")]
    public class AboutDentalController : Controller
    {
		
		private readonly WebsiteDentalContext _context;

        public AboutDentalController(WebsiteDentalContext context)
        {
            _context = context;
        }
		[Route("")]    // Map /About
		[Route("Index")] // Map /About/Index
		public IActionResult Index()
        {

            // Lấy danh sách ảnh từ bảng ImageGallery
            var imageGallery = _context.ImageGalleries.ToList();
            // Lấy dữ liệu từ bảng AboutDental
            var aboutDental = _context.AboutDentals.FirstOrDefault(); // Lấy 1 bản ghi

            // Truyền dữ liệu sang View
            ViewBag.ImageGallery = imageGallery;
            ViewBag.AboutDental = aboutDental;
            return View();
        }
    }
}
