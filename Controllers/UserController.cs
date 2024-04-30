using Microsoft.AspNetCore.Mvc;

namespace MuvekkilTakipSistemi.Controllers
{
	public class UserController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var adsoyad = HttpContext.Session.GetString("Adsoyad");
			var bSicilNo = HttpContext.Session.GetString("BSicilNo");
			var userId = HttpContext.Session.GetString("UserId");

			TempData["isim"] = adsoyad;

			if (adsoyad != null && bSicilNo != null && userId != null)
				return View();
			else
			{
				return Content("Bu sayfaya erişim izniniz bulunmamaktadır. " +
					"Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
			}
		}

        [HttpGet]
        public IActionResult Files()
        {
            var adsoyad = HttpContext.Session.GetString("Adsoyad");
            var bSicilNo = HttpContext.Session.GetString("BSicilNo");
            var userId = HttpContext.Session.GetString("UserId");

            TempData["isim"] = adsoyad;

            if (adsoyad != null && bSicilNo != null && userId != null)
                return View();
            else
            {
                return Content("Bu sayfaya erişim izniniz bulunmamaktadır. " +
                    "Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
            }
        }

        [HttpGet]
        public IActionResult Activities()
        {
            var adsoyad = HttpContext.Session.GetString("Adsoyad");
            var bSicilNo = HttpContext.Session.GetString("BSicilNo");
            var userId = HttpContext.Session.GetString("UserId");

            TempData["isim"] = adsoyad;

            if (adsoyad != null && bSicilNo != null && userId != null)
                return View();
            else
            {
                return Content("Bu sayfaya erişim izniniz bulunmamaktadır. " +
                    "Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
            }
        }

		[HttpGet]
		public IActionResult Settings()
		{
			var adsoyad = HttpContext.Session.GetString("Adsoyad");
			var bSicilNo = HttpContext.Session.GetString("BSicilNo");
			var userId = HttpContext.Session.GetString("UserId");

			TempData["isim"] = adsoyad;

			if (adsoyad != null && bSicilNo != null && userId != null)
				return View();
			else
			{
				return Content("Bu sayfaya erişim izniniz bulunmamaktadır. " +
					"Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
			}
		}

		public IActionResult Exit()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "Home");
		}



	}
}
