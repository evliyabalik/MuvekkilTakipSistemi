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

		[NonAction]
		private JsonResult GetClient()
		{
			return Json("");
		}

		[NonAction]
		private JsonResult GetFilesOnTable()
		{
			return Json("");
		}

		[NonAction]
		private JsonResult GetActivities()
		{
			return Json("");
		}

	}
}

