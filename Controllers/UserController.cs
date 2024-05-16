using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Models;

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


		public JsonResult GetClient()
		{
			var client = _context.Muvekkil.ToList();
			return Json(client);
		}

        public JsonResult InserClient(ClientInfo info)
        {
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId==id).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;

				_context.Muvekkil.Add(info);
				_context.SaveChanges();
				return Json("Müvekkil Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
        }

        [NonAction]
		private JsonResult GetFilesOnTable()
		{
			var files = _context.Dosyalar.ToList();
			return Json(files);
		}

		[NonAction]
		private JsonResult GetActivities()
		{
			var activities = _context.Islemler.ToList();
			return Json(activities);
		}

	}
}

