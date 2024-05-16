using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
	public class ClientInfo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }


		public string Ad_Unvan { get; set; }
        public string GrupAdi { get; set; }

        public string Tcno { get; set; }


		public string GSM { get; set; }

		public string Tel { get; set; } = "Boş";

		public string Vergi_Dairesi { get; set; } = "Boş";

		public string No { get; set; } = "Boş";

        public string Avukat { get; set; }

		public string Ozel_Alan { get; set; } = "Boş";
    }
}
