using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;

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
			ViewBag.GroupAdi = _context.ClientGroupNames.ToList();
			
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
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();

			var client = _context.Muvekkil.Where(u=> u.Avukat == avukat.Adsoyad).ToList();
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
		[HttpGet]
		public JsonResult EditClient(int id)
		{
			var client = _context.Muvekkil.Find(id);
			return Json(client);
		}

		[HttpPost]
		public JsonResult UpdateClient(ClientInfo client)
		{

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				client.Avukat = avukat.Adsoyad;
				_context.Muvekkil.Update(client);
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteClient(int id)
		{
			var client = _context.Muvekkil.Find(id);
			if (client!=null)
			{
				
				_context.Muvekkil.Remove(client);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

		
		public JsonResult GetFilesOnTable()
		{
            var id = HttpContext.Session.GetInt32("UserId");
            var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
            var files = _context.Dosyalar.Where(u=>u.Avukat==avukat.Adsoyad).ToList();
			return Json(files);
		}

        public JsonResult InserFile(Files info)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                info.Avukat = avukat.Adsoyad;

                _context.Dosyalar.Add(info);
                _context.SaveChanges();
                return Json("Müvekkil Başarı ile kaydedildi.");
            }

            return Json("Model Doğrulaması Başarısız.");
        }
        [HttpGet]
        public JsonResult EditFile(int id)
        {
            var client = _context.Dosyalar.Find(id);
            return Json(client);
        }

        [HttpPost]
        public JsonResult UpdateFile(Files file)
        {

            var id = HttpContext.Session.GetInt32("UserId");
            var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                file.Avukat = avukat.Adsoyad;
                _context.Dosyalar.Update(file);
                _context.SaveChanges();
                return Json("Güncelleme Başarılı");
            }
            return Json("Güncelleme Başarısız");
        }


        [HttpPost]
        public JsonResult DeleteFiles(int id)
        {
            var file = _context.Dosyalar.Find(id);
            if (file != null)
            {

                _context.Dosyalar.Remove(file);
                _context.SaveChanges();
                return Json("Silme Başarılı");
            }
            return Json("Silme Başarısız");
        }

        [NonAction]
		private JsonResult GetActivities()
		{
			var activities = _context.Islemler.ToList();
			return Json(activities);
		}

	}
}

