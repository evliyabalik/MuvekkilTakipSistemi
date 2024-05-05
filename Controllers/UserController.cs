using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.DatabaseContext;

namespace MuvekkilTakipSistemi.Controllers
{
	public class UserController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;
		private  string _adsoyad;


		public UserController(ILogger<HomeController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			_adsoyad = HttpContext.Session.GetString("Adsoyad");
			TempData["isim"] = _adsoyad;
			return View();
		}

        public IActionResult Files()
        {
			TempData["isim"] = _adsoyad;
			return View();
		}

        public IActionResult Activities()
        {
			TempData["isim"] = _adsoyad;
			return View();
		}


		public IActionResult Settings()
		{
			TempData["isim"] = _adsoyad;
			return View();
		}

		public IActionResult Exit()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "Home");
		}



	}
}

/*var adsoyad = HttpContext.Session.GetString("Adsoyad");
			var bSicilNo = HttpContext.Session.GetString("BSicilNo");
			var userId = HttpContext.Session.GetString("UserId");

			TempData["isim"] = adsoyad;

			if (adsoyad != null && bSicilNo != null && userId != null)
				return View();
			else
			{
				return Content("Bu sayfaya erişim izniniz bulunmamaktadır. " +
					"Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
			}*/
