using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.Models;
using System.Diagnostics;
using MuvekkilTakipSistemi.DatabaseContext;

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
                    var c = new ConcatTable()
                    {
                        Name_surname = Adsoyad,
                        Email = Email,
                        Tel = Telno,
                        Department = Department,
                        Message = Message.ToString(),
                        Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    };

                    _context.ConcatTable.Add(c);
                    _context.SaveChanges();
                    ViewData["contactvalue"] = "Kayýt eklendi.";
                }
                catch (Exception ex)
                {
                    ViewData["contactvalue"] = "Kayýt eklenmedi hata: " + ex.Message;
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
            var bSicilNo = _context.User.FirstOrDefault(u => u.BaroSicilNo == BaroSicilNo);
            var pass = _context.User.FirstOrDefault(u => u.Pass == Pass);

            if (bSicilNo != null && pass != null)
                ViewData["login"] = "Tebrikler Giriþ Baþarýlý";
            else
                ViewData["login"] = "Baro sicil no veya þifre hatalý";

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
                try
                {
                    var user = new User()
                    {
                        Tcno = Tcno,
                        BaroSicilNo = BaroSicilNo,
                        Adsoyad = Adsoyad,
                        Pass = Pass,
                        StatusId = 1,
                        IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    };

                    var userContact = new UserContact()
                    {
                        Email = Email,
                        Telno = Telno
                    };


                    if (_context.User.FirstOrDefault(u => u.Tcno == Tcno) != null && _context.User.FirstOrDefault(u => u.BaroSicilNo == BaroSicilNo) != null)
                    {
                        ViewData["registerValue"] = "Kullanýcý zaten kayýtlý";
                        ViewData["Class"] = "bg-warning";
                        return View();
                    }

                    _context.User.Add(user);
                    _context.SaveChanges();

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

            return View();
        }

        public IActionResult ForgotPass()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
