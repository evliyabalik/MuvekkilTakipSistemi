namespace MuvekkilTakipSistemi.Classes
{
	public class UploadFiles
	{
        public async static Task<string> UploadImage(IFormFile Profil, IWebHostEnvironment env, string folder)
        {
            string dosyaIsmi = "";
            if (Profil == null) return null;//Dosya yüklenmemişse çık
            if (Profil.Length > 0 && Path.GetExtension(Profil.FileName).ToUpper() == ".JPG" || Path.GetExtension(Profil.FileName).ToUpper() == ".JPEG" || Path.GetExtension(Profil.FileName).ToUpper() == ".PNG")//Dosya uzantısı
            {
                var wwwRootPath = env.WebRootPath;//Web serverinde root dizinini yolunu bul
                var uploadFolder = Path.Combine(wwwRootPath, folder); //root dizininde dosyanın yükleneceği klasörü al
                var dosyaYolu = Path.Combine(uploadFolder, Profil.FileName); //dosyanın ismi ile yüklenecek klasörü getir
                // İzin kontrolü
                if (!Directory.Exists(uploadFolder))//eğer dizin bulunmazsa
                {
                    Directory.CreateDirectory(uploadFolder); //dizini oluştur
                }
                if (!File.Exists(dosyaYolu)) //dosya dizinde yoksa
                {
                    using (var dosyaAkimi = new FileStream(dosyaYolu, FileMode.Create))//dosya oluştur modunda aç
                    {
                        await Profil.CopyToAsync(dosyaAkimi); //dosyayı kopyala
                    }
                }
                dosyaIsmi = Profil.FileName;// dosya ismini al
            }
            return dosyaIsmi;//dosya ismini dön
        }
    }
}
