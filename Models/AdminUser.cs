using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{
	public class AdminUser
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]	
		public string Adsoyad { get; set; }

		[Required]
		[StringLength(50)]
		public string Kullanici_adi { get; set;}

		[Required]
		[StringLength(50)]
		public string Pass { get; set; }

		[Required]
		[StringLength(50)]
		public string Email { get; set; }

        [Required]
        public int StatusId { get; set; }


    }
}
