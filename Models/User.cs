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
        public int StatusId { get; set; }
        public string IpAddress { get; set; }
    }
}
