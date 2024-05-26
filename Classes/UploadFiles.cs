namespace MuvekkilTakipSistemi.Classes
{
	public class UploadFiles
	{

        public async static Task<string> UploadImage(IFormFile Profil, IWebHostEnvironment env, string folder)
        {
            string dosyaIsmi = "";

            if (Profil == null) return null;

            if (Profil.Length > 0 && Path.GetExtension(Profil.FileName).ToUpper() == ".JPG" || Path.GetExtension(Profil.FileName).ToUpper() == ".JPEG" || Path.GetExtension(Profil.FileName).ToUpper() == ".PNG")
            {
                var wwwRootPath = env.WebRootPath;
                var uploadFolder = Path.Combine(wwwRootPath, folder);
                var dosyaYolu = Path.Combine(uploadFolder, Profil.FileName);

                // İzin kontrolü
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                if (!File.Exists(dosyaYolu))
                {
                    using (var dosyaAkimi = new FileStream(dosyaYolu, FileMode.Create))
                    {
                        await Profil.CopyToAsync(dosyaAkimi);
                    }
                }

                dosyaIsmi = Profil.FileName;
            }

            return dosyaIsmi;
        }


    }
}
