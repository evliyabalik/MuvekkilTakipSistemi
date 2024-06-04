using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MuvekkilTakipSistemi.Models
{
	public class Navigation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }
		public string MenuText { get; set; }
		public string ActionName { get; set; }
		public string ControlName { get; set; }
	}
}
