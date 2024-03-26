using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{
    public class ResetPassword
    {
        [Required]
        public string Email { get; set; }

        [Required]

        public string Pass { get; set; }

        [Required]
        public string PassR { get; set; }



        public  string token { get; set; }

    }
}
