using Microsoft.EntityFrameworkCore;
using MuvekkilTakipSistemi.Models;

namespace MuvekkilTakipSistemi.DatabaseContext
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }

        public DbSet<ContactTable> ContactTable { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserContact> UserContact { get; set; }
        public DbSet<Activies> Islemler { get; set; }
        public DbSet<ClientGroupName> ClientGroupNames { get; set; }
        public DbSet<ClientInfo> Muvekkil { get; set; }
        public DbSet<Files> Dosyalar { get; set; }
        public DbSet<Islem_Turu> Islem_Turleri { get; set; }
        public DbSet<Mahkemeler> Mahkemeleer { get; set; }
        public DbSet<Navigation> Menuler { get; set; }
        public DbSet<Yapilan_Islem> Yapilan_Islem { get; set; }
        public DbSet<OdemeSekli> Odeme_Sekli { get; set; }
        public DbSet<AdminUser> Admins { get; set; }
        //public DbSet<Statu> Status { get; set; }


      

    }
}
