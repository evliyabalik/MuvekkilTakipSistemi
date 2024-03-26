﻿using Microsoft.AspNetCore.Mvc;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Helper;
using MuvekkilTakipSistemi.Models;

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
            return View();
        }

        [HttpGet]
        public IActionResult Index(string token)
        {
            if (!HashHelper.VerifyToken(token))
            {
                TempData["Hata"] = "Bağlantının zamanı dolmuş";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(ResetPassword rPass, string OldPass)
        {

            if (OldPass != null)
            {
                try
                {
                    var userMail = _context.UserContact.Where(u => u.Email == rPass.Email).FirstOrDefault();
                    var user = _context.User.Where(u => u.UserId == userMail.UserId).FirstOrDefault();

                    if (user != null)
                    {
                        if (HashHelper.GetMd5Hash(OldPass) == user.Pass)
                        {
                            user.Pass = HashHelper.GetMd5Hash(rPass.Pass);
                            ViewData["resetPassValue"] = "Şifreniz başarıyla değiştirildi.";
                            ViewData["Class"] = "bg-success";
                            _context.SaveChanges();
                        }
                        else
                        {
                            ViewData["resetPassValue"] = "Eski şifreniz hatalı";
                            ViewData["Class"] = "bg-warning";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewData["resetPassValue"] = "Bir hata ile karşılaşıldı: " + ex.Message;
                    ViewData["Class"] = "bg-danger";

                }
            }


            return View();
        }


    }
}
