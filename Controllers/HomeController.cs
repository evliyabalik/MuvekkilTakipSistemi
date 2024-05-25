using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.Models;
using System.Diagnostics;
using MuvekkilTakipSistemi.DatabaseContext;

using System.Security.Cryptography;
using MuvekkilTakipSistemi.Helper;
using Microsoft.AspNetCore.Identity;
using System.Web;
using MuvekkilTakipSistemi.Classes;
using Microsoft.AspNetCore.Http;


namespace MuvekkilTakipSistemi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;


		public HomeController(ILogger<HomeController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Concat()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Concat(string Adsoyad, string Email, string Telno, string Department, string Message)
		{
			if (!String.IsNullOrEmpty(Adsoyad))
			{
				try
				{
					var c = new ContactTable()
					{
						Name_surname = HtmlEncodes.EncodeTurkishCharacters(Adsoyad.Trim()),
						Email = HtmlEncodes.EncodeTurkishCharacters(Email.Trim()),
						Tel = HtmlEncodes.EncodeTurkishCharacters(Telno.Trim()),
						Department = HtmlEncodes.EncodeTurkishCharacters(Department.Trim()),
						Message = Message.ToString(),
						Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString()
					};

					_context.ContactTable.Add(c);
					_context.SaveChanges();
					ViewData["contactvalue"] = "Kay�t eklendi.";
					ViewData["Class"] = "bg-success";
				}
				catch (Exception ex)
				{
					ViewData["contactvalue"] = "Kay�t eklenmedi hata: " + ex.Message;
					ViewData["Class"] = "bg-danger";
				}
			}


			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string BaroSicilNo, string Pass)
		{
			var bSicilNo = _context.User.FirstOrDefault(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim()));
			var pass = _context.User.FirstOrDefault(u => u.Pass == HashHelper.GetMd5Hash(Pass.Trim()));

			var bsicilNo = _context.User.Where(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim())).FirstOrDefault();

			HttpContext.Session.SetString("BSicilNo", bsicilNo.BaroSicilNo);
			HttpContext.Session.SetString("Adsoyad", bsicilNo.Adsoyad);
			HttpContext.Session.SetInt32("UserId", bsicilNo.UserId);

			HttpContext.Session.GetString("BSicilNo");
			HttpContext.Session.GetString("Adsoyad");
			HttpContext.Session.GetInt32("UserId");


			if (bSicilNo != null && pass != null)
			{
				/*ViewData["login"] = "Tebrikler Giri� Ba�ar�l�";
				ViewData["Class"] = "bg-success";*/

				return RedirectToAction("Index", "User");
			}
			else
			{
				ViewData["login"] = "Baro sicil no veya �ifre hatal�";
				ViewData["Class"] = "bg-warning";
			}


			return View();
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(string Tcno, string BaroSicilNo, string Adsoyad, string Telno,
			string Pass, string Email)
		{
			if (!String.IsNullOrEmpty(Tcno))
			{
				var getUser = _context.User.FirstOrDefault(u => u.Tcno == Tcno || u.BaroSicilNo==BaroSicilNo);
				var getUserContact = _context.UserContact.FirstOrDefault(uc => uc.Email == Email || uc.Telno == Telno);
				try
				{
					var user = new User()
					{
						Tcno = HtmlEncodes.EncodeTurkishCharacters(Tcno.Trim()),
						BaroSicilNo = HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim()),
						Adsoyad = HtmlEncodes.EncodeTurkishCharacters(Adsoyad.Trim()),
						Pass = HashHelper.GetMd5Hash(Pass.Trim()),
						StatusId = 1,
						IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
					};

					var userContact = new UserContact()
					{
						Email = HtmlEncodes.EncodeTurkishCharacters(Email.Trim()),
						Telno = HtmlEncodes.EncodeTurkishCharacters(Telno.Trim())
					};



					if (getUser!=null || getUserContact!=null)
					{
						ViewData["registerValue"] = "Kullan�c� zaten kay�tl�. �ifrenizi unuttuysan�z \"�ifremi Unuttum\" linkinden yenileyebilirsiniz.";
						ViewData["Class"] = "bg-warning";
						return View();
					}

					_context.User.Add(user);
					_context.SaveChanges();

					_context.UserContact.Add(userContact);
					_context.SaveChanges();

					ViewData["registerValue"] = "Kay�t Ba�ar�yla Eklendi";
					ViewData["Class"] = "bg-success";

				}
				catch (Exception ex)
				{
					ViewData["registerValue"] = "Kay�t Ba�ar�s�z Hata: " + ex;
					ViewData["Class"] = "bg-danger";
				}
			}

			return View();
		}

		public IActionResult ForgotPass()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ForgotPass(string BaroSicilNo, string Email)
		{
			var barosno = _context.User.FirstOrDefault(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim()));
			var email = _context.UserContact.FirstOrDefault(u => u.Email == HtmlEncodes.EncodeTurkishCharacters(Email.Trim()));



			if (barosno != null && email != null)
			{
				var adSoyad = _context.User.Select(u => u.Adsoyad).FirstOrDefault();
				var emailTo = _context.UserContact.Select(u => u.Email).FirstOrDefault();



				//Token olu�turma
				var token = HashHelper.GenerateToken(emailTo);
				var ResetLink = Url.Action("Index", "ResetPass", new { Email, token }, Request.Scheme);

				var message = $"Say�n {adSoyad}\n\r\n\r" +
					$"�ifrenizin s�f�rlanmas�n� talep ettiniz. Yeni bir �ifre se�mek i�in \"�ifremi s�f�rla\"ya t�klay�n �ifre.\r\n�ifrenizi de�i�tirmek ayn� zamanda API belirtecinizi de s�f�rlayacakt�r\n\r\n\r" +
					"Link 1 saat sonra aktif olmayacakt�r!" +

					$"\r\n\r\n{ResetLink}";

				if (MailModel.SendMessage(emailTo, "�ifre S�f�rlama", message))
				{
					ViewData["forgetValue"] = "Mailinize s�f�rlama ba�lant�s� iletilmi�tir.";
					ViewData["Class"] = "bg-success";
				}
				else
				{
					ViewData["forgetValue"] = "Bir hata ile kar��la��ld�: " + MailModel.GetErrorMessage();
					ViewData["Class"] = "bg-danger";
				}



			}
			else
			{
				ViewData["forgetValue"] = "Bilgilerinizi kontrol ediniz.";
				ViewData["Class"] = "bg-warning";
			}




			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
