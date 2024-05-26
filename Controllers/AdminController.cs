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
				/*ViewData["login"] = "Tebrikler Giriş Başarılı";
				ViewData["Class"] = "bg-success";*/

				return RedirectToAction("UserPage", "Admin");
			}
			else
			{
				ViewData["login"] = "Baro sicil no veya şifre hatalı";
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
		public IActionResult UserPage(string Del, string Edit)
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

			if (Edit != null)
			{
				ViewData["yaz"] = Edit;
				
			}

			return View(model);
		

			
		}

		public IActionResult AddPage()
		{
			return View();
		}

		public IActionResult AddAdmin()
		{
			return View();
		}

		public IActionResult Settings()
		{
			return View();
		}
	}
}
