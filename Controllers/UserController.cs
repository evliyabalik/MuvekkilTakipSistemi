﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MuvekkilTakipSistemi.Classes;
using MuvekkilTakipSistemi.DatabaseContext;
using MuvekkilTakipSistemi.Models;
using MuvekkilTakipSistemi.Models.ControlModels;

namespace MuvekkilTakipSistemi.Controllers
{
	public class UserController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MyContext _context;
		private string _adsoyad;


		public UserController(ILogger<HomeController> logger, MyContext context)
		{
			_logger = logger;
			_context = context;
		}


		//Clients Operations
		public IActionResult Index()
		{
			_adsoyad = HttpContext.Session.GetString("Adsoyad");
			TempData["isim"] = _adsoyad;
			ViewBag.GroupAdi = _context.ClientGroupNames.ToList();

			return View();
		}

		public IActionResult Files()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();

			TempData["isim"] = _adsoyad;
			ViewBag.Mahkeme = _context.Mahkemeleer.ToList();
			ViewBag.GroupAdi = _context.ClientGroupNames.ToList();
			return View();
		}

		public IActionResult Activities()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			TempData["isim"] = _adsoyad;
			ViewBag.Dosya = _context.Dosyalar.Where(d => d.Avukat == avukat.Adsoyad).ToList();
			ViewBag.IslemTuru = _context.Islem_Turleri.ToList();
			ViewBag.YapilanIslem = _context.Yapilan_Islem.ToList();
			ViewBag.OdemeTuru = _context.Odeme_Sekli.ToList();
			return View();
		}


		public IActionResult Settings()
		{
			TempData["isim"] = _adsoyad;
			return View();
		}

		public IActionResult Exit()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "Home");
		}


		public JsonResult GetClient()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();

			var client = _context.Muvekkil.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(client);
		}

		public JsonResult InserClient(ClientInfo info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;

				_context.Muvekkil.Add(info);
				_context.SaveChanges();
				return Json("Müvekkil Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}
		[HttpGet]
		public JsonResult EditClient(int id)
		{
			var client = _context.Muvekkil.Find(id);
			return Json(client);
		}

		[HttpPost]
		public JsonResult UpdateClient(ClientInfo client)
		{

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				client.Avukat = avukat.Adsoyad;
				_context.Muvekkil.Update(client);
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteClient(int id)
		{
			var client = _context.Muvekkil.Find(id);
			if (client != null)
			{

				_context.Muvekkil.Remove(client);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

		//File Operations
		public JsonResult GetFilesOnTable()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var files = _context.Dosyalar.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(files);
		}

		public JsonResult InserFile(Files info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var muvekkilGrubu = _context.Muvekkil.Where(u => u.Ad_Unvan == info.Muvekkil).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;
				info.Muvekkil_Grubu = muvekkilGrubu.GrupAdi;

				_context.Dosyalar.Add(info);
				_context.SaveChanges();
				return Json("Dosya Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}


		[HttpGet]
		public JsonResult EditFile(int id)
		{
			var file = _context.Dosyalar.Find(id);
			return Json(file);
		}

		[HttpPost]
		public JsonResult UpdateFile(Files file)
		{
			var muvekkilGrubu = _context.Muvekkil.Where(u => u.Ad_Unvan == file.Muvekkil).FirstOrDefault();

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				file.Avukat = avukat.Adsoyad;
				file.Muvekkil_Grubu = muvekkilGrubu.GrupAdi;

				_context.Dosyalar.Update(file);
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteFiles(int id)
		{
			var file = _context.Dosyalar.Find(id);
			if (file != null)
			{

				_context.Dosyalar.Remove(file);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

		//Activities Operations
		public JsonResult GetActivities()
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var activities = _context.Islemler.Where(u => u.Avukat == avukat.Adsoyad).ToList();
			return Json(activities);
		}


		public JsonResult InsertActivities(Activies info)
		{
			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var file = _context.Dosyalar.Where(f => f.DosyaNo == info.Dosya).FirstOrDefault();

			if (!ModelState.IsValid)
			{
				info.Avukat = avukat.Adsoyad;
				info.Konusu = file.Konusu;
				info.Mahkeme = file.Mahkeme;
				_context.Islemler.Add(info);
				_context.SaveChanges();
				return Json("Dosya Başarı ile kaydedildi.");
			}

			return Json("Model Doğrulaması Başarısız.");
		}


		[HttpGet]
		public JsonResult EditActivities(int id)
		{
			var activities = _context.Islemler.Find(id);
			return Json(activities);
		}

		[HttpPost]
		public JsonResult UpdateActivities(Activies file)
		{

			var id = HttpContext.Session.GetInt32("UserId");
			var avukat = _context.User.Where(u => u.UserId == id).FirstOrDefault();
			var fileInfo = _context.Dosyalar.Where(f => f.DosyaNo == file.Dosya).FirstOrDefault();
			if (!ModelState.IsValid)
			{
				file.Avukat = avukat.Adsoyad;
				file.Konusu = fileInfo.Konusu;
				file.Mahkeme = fileInfo.Mahkeme;
				_context.Islemler.Update(file);
				_context.SaveChanges();
				return Json("Güncelleme Başarılı");
			}
			return Json("Güncelleme Başarısız");
		}


		[HttpPost]
		public JsonResult DeleteActivities(int id)
		{
			var activities = _context.Islemler.Find(id);
			if (activities != null)
			{

				_context.Islemler.Remove(activities);
				_context.SaveChanges();
				return Json("Silme Başarılı");
			}
			return Json("Silme Başarısız");
		}

	}
}

