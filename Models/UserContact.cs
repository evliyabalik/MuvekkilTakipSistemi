using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{
    public class UserContact
    {
        [Key]
        public int UserId { get; set; }
        public string Telno { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
    }
}
