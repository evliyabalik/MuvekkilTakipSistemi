﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Muvekkil;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
}
