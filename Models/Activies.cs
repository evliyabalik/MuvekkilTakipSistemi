using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MuvekkilTakipSistemi.Models
{
	public class Activies
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }
		[Required]
		public string Tarih { get; set; } = DateTime.Now.ToString();
		[Required]
		public int Evrak_No { get; set; }
		[Required]
		public string Islem_Turu { get; set; }
		[Required]
		public string Yapilan_Islem { get; set; }
		[Required]
		public string Muvekkil { get; set; }
		[Required]
		public string Dosya { get; set; }
		[Required]
		public string Mahkeme { get; set; }
		[Required]
		public string Konusu { get; set; }
		[Required]
		public string Avukat { get; set; }
		[Required]
		public string Odeme_Sekli { get; set; }
		[Required]
		public decimal Islem_Tutari { get; set; }
		public string? Aciklama { get; set; }
	}
}
