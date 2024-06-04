using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MuvekkilTakipSistemi.Models
{
	public class Mahkemeler
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
