using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{
	public class Page
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string PageTitle { get; set; }
		public string Date { get; set; } = DateTime.Now.ToString();
		public string User { get; set; }
	}
}
