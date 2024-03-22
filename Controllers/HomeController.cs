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
            if(!String.IsNullOrEmpty(Adsoyad))
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
                    ViewData["contactvalue"] = "Kayýt eklenmedi hata: "+ex.Message;
                }
            }
            

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Tcno, string BaroSicilNo, string Adsoyad, string Telno,
            string Pass, string PassR, string Email, string EmailR)
        {


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
