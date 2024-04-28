using System.ComponentModel.DataAnnotations;

namespace MuvekkilTakipSistemi.Models
{
    public class ContactTable
    {
        [Key]
        public int id { get; set; }

        public string Name_surname { get; set; }
        public string Email  { get; set; }
        public string Tel  { get; set; }
        public string Department  { get; set; }
        public string Message  { get; set; }
        public string Ip_address { get; set; }
    }
}
