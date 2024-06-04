using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Helper;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;
namespace MuvekkilTakipSistemi.Controllers
{
	public class ResetPassController : Controller
	{
		private readonly ILogger<ResetPassController> _logger;
		private readonly MyContext _context;
		public ResetPassController(ILogger<ResetPassController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
            DefaultModels model = new DefaultModels()
            {
                settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
            };
            return View(model);
        }
		//Gelen tokken alınıp geçerliliği kontrol ediliyor
		[HttpGet]
		public IActionResult Index(string token)
		{
			if (!HashHelper.VerifyToken(token))//Tokken geçerli mi
			{
				TempData["Hata"] = "Bağlantının zamanı dolmuş";
			}
            DefaultModels model = new DefaultModels()
            {
                settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
            };
            return View(model);
        }

		//Şifre değiştir.
		[HttpPost]
		public IActionResult Index(ResetPassword rPass)
		{
			try
			{
				var userMail = _context.UserContact.Where(u => u.Email == rPass.Email.Trim()).FirstOrDefault();
				var user = _context.User.Where(u => u.UserId == userMail.UserId).FirstOrDefault();
				if (user != null)
				{
					user.Pass = HashHelper.GetMd5Hash(rPass.Pass);
					ViewData["resetPassValue"] = "Şifreniz başarıyla değiştirildi.";
					ViewData["Class"] = "bg-success";
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				ViewData["resetPassValue"] = "Bir hata ile karşılaşıldı: " + ex.Message;
				ViewData["Class"] = "bg-danger";
			}
            DefaultModels model = new DefaultModels()
            {
                settings = _context.SiteSettings.Where(s => s.Id == 1).ToList(),
            };
            return View(model);
        }
	}
}
