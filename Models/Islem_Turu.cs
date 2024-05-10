using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
	public class Islem_Turu
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
