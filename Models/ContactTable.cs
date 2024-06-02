using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
    public class ContactTable
    {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        public string Name_surname { get; set; }
        public string Email  { get; set; }
        public string Tel  { get; set; }
        public int Department  { get; set; }
        public string Subject { get; set; }
        public string Message  { get; set; }
		public DateTime Date { get; set; }
		public string Ip_address { get; set; }


	}
}
