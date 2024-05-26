using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Helper;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;

namespace MuvekkilTakipSistemi.Controllers
{
	public class UserController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;
		private string _adsoyad;
		private int? _userId;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public UserController(ILogger<HomeController> logger, MyContext context, IWebHostEnvironment hostingEnvironment)
		{
			_logger = logger;
			_context = context;
			_hostingEnvironment= hostingEnvironment;
		}


		//Clients Operations
		public IActionResult Index()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			_adsoyad = HttpContext.Session.GetString("Adsoyad");
			TempData["isim"] = _adsoyad;
			var getUser = _context.User.Find(id);
			ViewData["resim"] = getUser.Profil_Resim;
			ViewBag.GroupAdi = _context.ClientGroupNames.ToList();

			return View();
		}

		public IActionResult Files()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var getUser = _context.User.Find(id);
			ViewData["resim"] = getUser.Profil_Resim;
			TempData["isim"] = _adsoyad;
			ViewBag.Mahkeme = _context.Mahkemeleer.ToList();
			ViewBag.GroupAdi = _context.ClientGroupNames.ToList();
			return View();
		}

		public IActionResult Activities()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			TempData["isim"] = _adsoyad;
			var getUser = _context.User.Find(id);
			ViewData["resim"] = getUser.Profil_Resim;
			ViewBag.Dosya = _context.Dosyalar.Where(d => d.Avukat == avukat.Adsoyad).ToList();
			ViewBag.IslemTuru = _context.Islem_Turleri.ToList();
			ViewBag.YapilanIslem = _context.Yapilan_Islem.ToList();
			ViewBag.OdemeTuru = _context.Odeme_Sekli.ToList();
			return View();
		}


		public IActionResult Settings()
		{
			TempData["isim"] = _adsoyad;
			_userId = HttpContext.Session.GetInt32("UserId");
			var getUser= _context.User.Find(_userId);
			ViewData["resim"] = getUser.Profil_Resim;
			var model = _context.User.Where(u => u.UserId == _userId).FirstOrDefault();
			return View(model);
		}

		[HttpPost]
		public async  Task<IActionResult> Settings(User user, string PassR, IFormFile Profil)
		{

			TempData["isim"] = _adsoyad;
            _userId = HttpContext.Session.GetInt32("UserId");
			 var getUser = _context.User.Find(_userId);
            if (Profil != null && Profil.Length > 0)
            {
				getUser.Profil_Resim = await UploadFiles.UploadImage(Profil, _hostingEnvironment, "Yukle");
            }
			else{
                TempData["sonuc"] = "Profil resmi kaydedilirken bir hata ile karşılaşıldı";
                TempData["class"] = "bg-danger";
            }
			if(user.Pass!=null && user.Pass==PassR)
			{
				getUser.Pass = HtmlEncodes.EncodeTurkishCharacters(HashHelper.GetMd5Hash(user.Pass.Trim()));
			}
			else
			{
				TempData["sonuc"] = "Şifreler Uyuşmuyor";
                TempData["class"] = "bg-danger";
			}
            _context.User.Update(getUser);
			_context.SaveChanges();

            TempData["sonuc"] = "Değişiklikler başarıyla uygulandı";
            TempData["class"] = "bg-success";
            return RedirectToAction("Settings", "User");
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

			var client = _context.Muvekkil.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(client);
		}

		public JsonResult InserClient(ClientInfo info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var isClient = _context.Muvekkil.FirstOrDefault(m => m.Tcno == info.Tcno || m.GSM==info.GSM);

			if (!ModelState.IsValid)
			{
				if (isClient!=null) { return Json("Müvekkil zaten kayıtlı."); }

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
			if (client != null)
			{

				_context.Muvekkil.Remove(client);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

		//File Operations
		public JsonResult GetFilesOnTable()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var files = _context.Dosyalar.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(files);
		}

		public JsonResult InserFile(Files info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var muvekkilGrubu = _context.Muvekkil.Where(u => u.Ad_Unvan == info.Muvekkil).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;
				info.Muvekkil_Grubu = muvekkilGrubu.GrupAdi;

				_context.Dosyalar.Add(info);
				_context.SaveChanges();
				return Json("Dosya Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}


		[HttpGet]
		public JsonResult EditFile(int id)
		{
			var file = _context.Dosyalar.Find(id);
			return Json(file);
		}

		[HttpPost]
		public JsonResult UpdateFile(Files file)
		{
			var muvekkilGrubu = _context.Muvekkil.Where(u => u.Ad_Unvan == file.Muvekkil).FirstOrDefault();

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				file.Avukat = avukat.Adsoyad;
				file.Muvekkil_Grubu = muvekkilGrubu.GrupAdi;

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

		//Activities Operations
		public JsonResult GetActivities()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var activities = _context.Islemler.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(activities);
		}


		public JsonResult InsertActivities(Activies info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var file = _context.Dosyalar.Where(f => f.DosyaNo == info.Dosya).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;
				info.Konusu = file.Konusu;
				info.Mahkeme = file.Mahkeme;
				_context.Islemler.Add(info);
				_context.SaveChanges();
				return Json("Dosya Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}


		[HttpGet]
		public JsonResult EditActivities(int id)
		{
			var activities = _context.Islemler.Find(id);
			return Json(activities);
		}

		[HttpPost]
		public JsonResult UpdateActivities(Activies file)
		{

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var fileInfo = _context.Dosyalar.Where(f => f.DosyaNo == file.Dosya).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				file.Avukat = avukat.Adsoyad;
				file.Konusu = fileInfo.Konusu;
				file.Mahkeme = fileInfo.Mahkeme;
				_context.Islemler.Update(file);
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteActivities(int id)
		{
			var activities = _context.Islemler.Find(id);
			if (activities != null)
			{

				_context.Islemler.Remove(activities);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

	}
}

