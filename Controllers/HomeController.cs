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
using MuvekkilTakipSistemi.Models.ControlModels;


namespace MuvekkilTakipSistemi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context; //Veritabaný deðiþkeni
		private const string SiteUrl = "http://evliya.somee.com"; //Sýfýrlama iþlemlerinde sitenin baðlantýsýný kullanarak tokken oluþturmak için bir deðiþken


		public HomeController(ILogger<HomeController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
		}

		//Ýndex, Sitenin ayarlar tablosundan veri çekmek için model gönderiyor
		public IActionResult Index()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}

		public IActionResult About()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}

		public IActionResult Concat()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}


		//Ýletiþim sayfasýndan gelen verileri alýp Contact tablosuna verileri kaydediyor
		[HttpPost]
		public IActionResult Concat(string Adsoyad, string Email, string Telno, int Department, string Subject, string Message)
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
						Department = Department,
						Subject = HtmlEncodes.EncodeTurkishCharacters(Subject.Trim()),
						Message = Message.ToString(),
						Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString()
					};

					_context.ContactTable.Add(c);
					_context.SaveChanges();
					ViewData["contactvalue"] = "Mesajýnýz kaydedildi.";
					ViewData["Class"] = "bg-success";
				}
				catch (Exception ex)
				{
					ViewData["contactvalue"] = "Mesajýnýz kaydedilirken hata: " + ex.Message;
					ViewData["Class"] = "bg-danger";
				}
			}


			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),

			};

			return View(model);
		}

		public IActionResult Login()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),

			};

			return View(model);
		}

		//Giriþ iþlemlerini sorgulayýp giriþ için gerekli verileri gönderiyor
		[HttpPost]
		public IActionResult Login(string BaroSicilNo, string Pass)
		{
			var bSicilNo = _context.User.FirstOrDefault(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim()));
			var pass = _context.User.FirstOrDefault(u => u.Pass == HashHelper.GetMd5Hash(Pass.Trim()));

			var bsicilNo = _context.User.Where(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim())).FirstOrDefault();


			if (bSicilNo != null && pass != null)
			{
				//Session oluþturma
				HttpContext.Session.SetString("BSicilNo", bsicilNo.BaroSicilNo);
				HttpContext.Session.SetString("Adsoyad", bsicilNo.Adsoyad);
				HttpContext.Session.SetInt32("UserId", bsicilNo.UserId);

				return RedirectToAction("Index", "User");
			}
			else
			{
				ViewData["login"] = "Baro sicil no veya þifre hatalý";
				ViewData["Class"] = "bg-warning";
			}

			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),

			};

			return View(model);

		}


		public IActionResult Register()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}

		//Kayýt iþlemi, veri kontrolü saðladýktan sonra girilen bilgiler tabloda bulunmuyorsa yeni kayýt ekleniyor.
		[HttpPost]
		public IActionResult Register(string Tcno, string BaroSicilNo, string Adsoyad, string Telno,
			string Pass, string Email)
		{

            DefaultModels model = new DefaultModels()
            {
                status = _context.Status.ToList(),
                settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
            };
            if (!String.IsNullOrEmpty(Tcno))
			{
				var getUser = _context.User.FirstOrDefault(u => u.Tcno == Tcno || u.BaroSicilNo == BaroSicilNo);
				var getUserContact = _context.UserContact.FirstOrDefault(uc => uc.Email == Email || uc.Telno == Telno);
				if (getUser != null || getUserContact != null)
				{
					ViewData["registerValue"] = "Kullanýcý zaten kayýtlý. Þifrenizi unuttuysanýz \"Þifremi Unuttum\" linkinden yenileyebilirsiniz.";
					ViewData["Class"] = "bg-warning";
					return View(model);
				}
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
					_context.User.Add(user);
					_context.SaveChanges();
					var userContact = new UserContact()
					{
						UserId = user.UserId,
						Email = HtmlEncodes.EncodeTurkishCharacters(Email.Trim()),
						Telno = HtmlEncodes.EncodeTurkishCharacters(Telno.Trim())
					};
					_context.UserContact.Add(userContact);
					_context.SaveChanges();
					ViewData["registerValue"] = "Kayýt Baþarýyla Eklendi";
					ViewData["Class"] = "bg-success";
				}
				catch (Exception ex)
				{
					ViewData["registerValue"] = "Kayýt Baþarýsýz Hata: " + ex;
					ViewData["Class"] = "bg-danger";
				}
			}
			
			return View(model);
		}

		public IActionResult ForgotPass()
		{
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}

		//Þifremi unuttum 
		[HttpPost]
		public IActionResult ForgotPass(string BaroSicilNo, string Email)
		{
			//Girilen bilgiler veritabanýnda var mý
			var barosno = _context.User.FirstOrDefault(u => u.BaroSicilNo == HtmlEncodes.EncodeTurkishCharacters(BaroSicilNo.Trim()));
			var email = _context.UserContact.FirstOrDefault(u => u.Email == HtmlEncodes.EncodeTurkishCharacters(Email.Trim()));

			if (barosno != null && email != null)//Bilgiler boþ mu
			{
				//Bilgileri çek
				var adSoyad = _context.User.Select(u => u.Adsoyad).FirstOrDefault();
				var emailTo = _context.UserContact.Select(u => u.Email).FirstOrDefault();

				//Token oluþturma
				var token = HashHelper.GenerateToken(emailTo);//Token oluþtur.
				var ResetLink = $"{SiteUrl}{Url.Action("Index", "ResetPass", new { Email, token })}";//Tokeni al ve bir link oluþtur.
																									 //Mesaj
				var message = $"Sayýn {adSoyad}\n\r\n\r" +
					$"Þifrenizin sýfýrlanmasýný talep ettiniz. Yeni bir þifre seçmek için \"Þifremi sýfýrla\"ya týklayýn þifre.\r\nÞifrenizi deðiþtirmek ayný zamanda API belirtecinizi de sýfýrlayacaktýr\n\r\n\r" +
					"Link 1 saat sonra aktif olmayacaktýr!" +
					$"\r\n\r\n{ResetLink}";

				if (MailModel.SendMessage(emailTo, "Þifre Sýfýrlama", message))//Mesaj gönder
				{
					ViewData["forgetValue"] = "Mailinize sýfýrlama baðlantýsý iletilmiþtir.";
					ViewData["Class"] = "bg-success";
				}
				else
				{
					ViewData["forgetValue"] = "Bir hata ile karþýlaþýldý: " + MailModel.GetErrorMessage();
					ViewData["Class"] = "bg-danger";
				}
			}
			else
			{
				ViewData["forgetValue"] = "Bilgilerinizi kontrol ediniz.";
				ViewData["Class"] = "bg-warning";
			}
			DefaultModels model = new DefaultModels()
			{
				status = _context.Status.ToList(),
				settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
			};
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
