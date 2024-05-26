using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{

    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Tcno { get; set; }
        public string BaroSicilNo { get; set; }
        public string Adsoyad { get; set; }
        public string Pass { get; set; }
        public int StatusId { get; set; }
        public string IpAddress { get; set; }
        public DateTime? RegisterDate { get; set; } = DateTime.Now;
        public string? Profil_Resim { get; set; }

	}
}
