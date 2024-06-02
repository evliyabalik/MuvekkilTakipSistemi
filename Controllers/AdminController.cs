using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Helper;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;
using System.Collections.Generic;

namespace MuvekkilTakipSistemi.Controllers
{
	public class AdminController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;


		public AdminController(ILogger<HomeController> logger, MyContext context, IWebHostEnvironment hostingEnvironment)
		{
			_logger = logger;
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{

			TempData["UserStatus"] = HttpContext.Session.GetInt32("Status");
			TempData.Keep("UserStatus");
			return View();
		}

		[HttpPost]
		public IActionResult Index(string Kullanici_Adi, string Pass)
		{



			var kull_adi = _context.Admins.FirstOrDefault(u => u.Kullanici_adi == HtmlEncodes.EncodeTurkishCharacters(Kullanici_Adi.Trim()));
			var pass = _context.Admins.FirstOrDefault(u => u.Pass == HashHelper.GetMd5Hash(Pass.Trim()));

			var id = _context.Admins.Where(u => u.Kullanici_adi == Kullanici_Adi).FirstOrDefault();

			if (kull_adi != null && pass != null)
			{
				HttpContext.Session.SetInt32("AdminId", id.Id);
				HttpContext.Session.SetInt32("Status", id.StatusId);

				if (HttpContext.Session.GetInt32("Status") == 2)
				{
					return RedirectToAction("UserPage", "Admin");
				}
				else
				{
					return RedirectToAction("Message", "Admin");
				}
			}
			else
			{
				ViewData["login"] = "Kullanıcı adı veya şifre hatalı";
				ViewData["Class"] = "bg-warning";
			}


			TempData["UserStatus"] = HttpContext.Session.GetInt32("Status");
			TempData.Keep("UserStatus");


			return View();
		}
		public IActionResult UserPage()
		{

			UserAndContact model = new UserAndContact()
			{
				UserAndContacts = (List<UserAndContact>)(from user in _context.User
														 join contact in _context.UserContact on user.UserId equals contact.UserId
														 select new UserAndContact { Users = user, UserContact = contact }).ToList()
			};


			var statu = HttpContext.Session.GetInt32("Status");
			if (statu == 2)
			{
				return View(model);

			}
			return Content("Yetkiniz Bulunmamaktadır.");

			
		}

		[HttpGet]
		public IActionResult UserPage(string Del)
		{

			UserAndContact model = new UserAndContact()
			{
				UserAndContacts = (List<UserAndContact>)(from user in _context.User
														 join contact in _context.UserContact on user.UserId equals contact.UserId
														 select new UserAndContact { Users = user, UserContact = contact }).ToList()
			};


			if (Del != null)
			{
				var user = _context.User.Where(u => u.UserId == Convert.ToInt32(Del)).FirstOrDefault();
				var userContact = _context.UserContact.Where(u => u.UserId == Convert.ToInt32(Del)).FirstOrDefault();

				if (user != null && userContact != null)
				{
					_context.User.Remove(user);
					_context.SaveChanges();

					_context.UserContact.Remove(userContact);
					_context.SaveChanges();

				}
				Del = null;
				return RedirectToAction("UserPage", "Admin");
			}



			var statu = HttpContext.Session.GetInt32("Status");
			if (statu == 2)
			{
				return View(model);

			}
			return Content("Yetkiniz Bulunmamaktadır.");

		}

		public IActionResult AddAdmin()
		{
			TempData["UserStatus"] = HttpContext.Session.GetInt32("Status");
			TempData.Keep("UserStatus");

			ViewBag.Statu = _context.Status.ToList();

			return View();
		}


		public JsonResult GetAdmin()
		{
			var statu = HttpContext.Session.GetInt32("Status");
			if (statu == 2)
			{
				var adm = _context.Admins.ToList();
				return Json(adm);
			}
			var admin = _context.Admins.Where(a => a.StatusId == statu).ToList();
			return Json(admin);
		}


		public JsonResult InsertAdmin(AdminUser info)
		{
			var isAdmin = _context.Admins.FirstOrDefault(m => m.Adsoyad == info.Adsoyad || m.Email == info.Email);

			if (ModelState.IsValid)
			{
				if (isAdmin != null) { return Json("Admin zaten kayıtlı."); }
				info.Pass = HashHelper.GetMd5Hash(HtmlEncodes.EncodeTurkishCharacters(info.Pass.Trim()));

				_context.Admins.Add(info);
				_context.SaveChanges();
				return Json("Admin Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}
		[HttpGet]
		public JsonResult EditAdmin(int id)
		{
			var admin = _context.Admins.Find(id);
			return Json(admin);
		}

		[HttpPost]
		public JsonResult UpdateAdmin(AdminUser admin)
		{
			if (ModelState.IsValid)
			{
				if (admin.Pass.Length < 64)
				{
					admin.Pass = HashHelper.GetMd5Hash(HtmlEncodes.EncodeTurkishCharacters(admin.Pass.Trim()));
				}


				_context.Attach(admin);
				_context.Entry(admin).State = EntityState.Modified;
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteAdmin(int id)
		{
			var admin = _context.Admins.Find(id);
			if (admin != null)
			{

				_context.Admins.Remove(admin);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}


		public IActionResult Settings()
		{
			var statu = HttpContext.Session.GetInt32("Status");
			if (statu == 2 || statu == 4)
			{
				return View();
				
			}
			return Content("Yetkiniz Bulunmamaktadır.");

		}

		[HttpPost]
		public async Task<IActionResult> AddBanner(SiteSettings setting, IFormFile Banner)
		{
			if (Banner != null && Banner.Length > 0)
			{
				setting.Id = 1;
				setting.Banner = await UploadFiles.UploadImage(Banner, _hostingEnvironment, "images");
			}
			else
			{
				TempData["sonuc"] = "Banner kaydedilirken bir hata ile karşılaşıldı";
				TempData["class"] = "bg-danger";
			}

			_context.SiteSettings.Update(setting);
			_context.SaveChanges();

			TempData["sonuc"] = "Değişiklikler başarıyla uygulandı";
			TempData["class"] = "bg-success";

			return RedirectToAction("Settings", "Admin");
		}


		[HttpPost]
		public IActionResult AddMahkeme(Mahkemeler mahkeme)
		{
			try
			{
				var dizi = mahkeme.Name.Split(",");


				foreach (var item in dizi)
				{
					Mahkemeler m = new Mahkemeler();
					m.Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.Mahkemeleer.Add(m);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "Mahkeme tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "Mahkeme tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}

			return RedirectToAction("Settings", "Admin");
		}

		[HttpPost]
		public IActionResult AddIslemTuru(Islem_Turu islem)
		{
			try
			{
				var dizi = islem.Name.Split(",");


				foreach (var item in dizi)
				{
					Islem_Turu islemT = new Islem_Turu();
					islemT.Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.Islem_Turleri.Add(islemT);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "İşlem Türü tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "İşlem Türü tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}

			return RedirectToAction("Settings", "Admin");
		}

		[HttpPost]
		public IActionResult AddYapilanIslem(Yapilan_Islem islem)
		{
			try
			{
				var dizi = islem.Name.Split(",");


				foreach (var item in dizi)
				{
					Yapilan_Islem Yislem = new Yapilan_Islem();
					Yislem.Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.Yapilan_Islem.Add(Yislem);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "Yapılan işlemler tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "Yapılan İşlemler tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}

			return RedirectToAction("Settings", "Admin");
		}

		[HttpPost]
		public IActionResult AddOdemeSekli(OdemeSekli odeme)
		{
			try
			{
				var dizi = odeme.Name.Split(",");


				foreach (var item in dizi)
				{
					OdemeSekli OdemeS = new OdemeSekli();
					OdemeS.Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.Odeme_Sekli.Add(OdemeS);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "Ödeme Şekli tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "Ödeme Şekli tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}

			return RedirectToAction("Settings", "Admin");
		}


		[HttpPost]
		public IActionResult AddDepartment(Statu status)
		{
			try
			{
				var dizi = status.Name.Split(",");


				foreach (var item in dizi)
				{
					Statu statu = new Statu();
					statu.Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.Status.Add(statu);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "Statu tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "Statu tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}

			return RedirectToAction("Settings", "Admin");
		}

		[HttpPost]
		public IActionResult AddClientGroup(ClientGroupName client)
		{
			try
			{
				var dizi = client.Group_Name.Split(",");


				foreach (var item in dizi)
				{
					ClientGroupName clientG = new ClientGroupName();
					clientG.Group_Name = HtmlEncodes.EncodeTurkishCharacters(item.Trim());
					_context.ClientGroupNames.Add(clientG);
				}
				_context.SaveChanges();

				TempData["sonuc"] = "Müvekkil Grubu tablosuna başarılı bir şekilde eklendi";
				TempData["class"] = "bg-success";
			}
			catch (Exception ex)
			{

				TempData["sonuc"] = "Müvekkil Grubu tablosuna eklenirken bir hata oluştu. Hata: " + ex.Message;
				TempData["class"] = "bg-danger";
			}
			return RedirectToAction("Settings", "Admin");
		}

		public IActionResult Message()
		{

			return View();
		}

		public JsonResult GetMessage()
		{
			var status = HttpContext.Session.GetInt32("Status");
			if (status != 2)
			{
				var message = _context.ContactTable.Where(m => m.Department == status).ToList();
				return Json(message);
			}
			else
			{
				var message = _context.ContactTable.ToList();
				return Json(message);
			}

		}

		[HttpGet]
		public JsonResult ReadMessage(int id)
		{
			var message = _context.ContactTable.Find(id);
			return Json(message);
		}




		public IActionResult Exit()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Admin");
		}
	}
}
