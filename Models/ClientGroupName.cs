using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuvekkilTakipSistemi.Models
{
	public class ClientGroupName
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }

		[Required]
		public string Group_Name { get; set; }
	}
}
