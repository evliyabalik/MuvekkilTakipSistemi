using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Helper;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;

namespace MuvekkilTakipSistemi.Controllers
{
	public class AdminController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;


		public AdminController(ILogger<HomeController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
			
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(string Kullanici_Adi, string Pass)
		{
			


			var kull_adi = _context.Admins.FirstOrDefault(u => u.Kullanici_adi == HtmlEncodes.EncodeTurkishCharacters(Kullanici_Adi.Trim()));
			var pass = _context.Admins.FirstOrDefault(u => u.Pass == HashHelper.GetMd5Hash(Pass.Trim()));

			var id = _context.Admins.Where(u => u.Kullanici_adi == Kullanici_Adi).FirstOrDefault();

			HttpContext.Session.SetInt32("AdminId", id.Id);




			if (kull_adi != null && pass != null)
			{
				return RedirectToAction("UserPage", "Admin");
			}
			else
			{
				ViewData["login"] = "Kullanıcı adı veya şifre hatalı";
				ViewData["Class"] = "bg-warning";
			}

			


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


			return View(model);
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
				var user = _context.User.Where(u=>u.UserId==Convert.ToInt32(Del)).FirstOrDefault();
				var userContact = _context.UserContact.Where(u => u.UserId == Convert.ToInt32(Del)).FirstOrDefault();

				if(user!=null && userContact != null)
				{
					_context.User.Remove(user);
					_context.SaveChanges();

					_context.UserContact.Remove(userContact);
					_context.SaveChanges();
					
				}
				Del = null;
				return RedirectToAction("UserPage", "Admin");
			}

			

			return View(model);
		

			
		}

		public IActionResult AddAdmin()
		{
			

			ViewBag.Statu = _context.Status.ToList();

			return View();
		}


		public JsonResult GetAdmin()
		{
			var admin = _context.Admins.ToList();
			return Json(admin);
		}


		public JsonResult InsertAdmin(AdminUser info)
		{
			var isAdmin = _context.Admins.FirstOrDefault(m => m.Adsoyad == info.Adsoyad || m.Email == info.Email);

			if (ModelState.IsValid)
			{
				if (isAdmin != null) { return Json("Müvekkil zaten kayıtlı."); }
				info.Pass = HashHelper.GetMd5Hash(HtmlEncodes.EncodeTurkishCharacters(info.Pass.Trim()));
				
				_context.Admins.Add(info);
				_context.SaveChanges();
				return Json("Müvekkil Başarı ile kaydedildi.");
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
				_context.Entry(admin).State=EntityState.Modified;
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
			
			return View();
		}

		public IActionResult Exit()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Admin");
		}
	}
}
