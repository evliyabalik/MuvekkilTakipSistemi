using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
	public class Files
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }

		[Required]
		public string DosyaNo { get; set; }

		[Required]
		public string Konusu { get; set;}

		[Required]
		public string Avukat { get; set; }

		[Required]
		public string Mahkeme { get; set; }

		[Required]
		public string Muvekkil { get; set; }

		[Required]
		public string Muvekkil_Grubu { get; set; }

		[Required]
		public string Adres { get; set; }


		public string? Adi_telefon { get; set; }

		public string? Adi_telefon2 { get; set; }


		public string? Ozel_Alan { get; set; }


		public string? Ozel_Alan2 { get; set; }

		[Required]
		public string Referans { get; set; }


		public string Ucret_Sozlesmesi { get; set; } 


		public string? Sozlesme_No { get; set; }


		public string? Serbest_Meslek_Makbuzu { get; set; } 

		public string? Dosya_Durumu { get; set; }

	}
}

