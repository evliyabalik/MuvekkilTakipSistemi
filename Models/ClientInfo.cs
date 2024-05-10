using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
	public class ClientInfo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }

		[Required]	
		public string Ad_Unvan { get; set; }

		[Required]
		public string Tc_no { get; set; }

		[Required]
		public string GSM { get; set; }

		public string Tel { get; set; }

		public string Vergi_Dairesi { get; set;}

		public string No { get; set;}

        public int Avukat { get; set; }

		public int Ozel_Alan { get; set; }
    }
}
