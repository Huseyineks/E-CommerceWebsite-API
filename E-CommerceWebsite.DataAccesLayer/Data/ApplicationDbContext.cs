using E_CommerceWebsite.EntitiesLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace E_CommerceWebsite.DataAccesLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppUserRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;initial catalog=E-CommerceWebsiteDB;integrated Security=true; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.Property(i => i.Size).HasConversion<string>();
            });

            builder.Entity<Order>(entity =>
            {
                //entity.Property(i => i.Size).HasConversion<string>();
                //entity.Property(i => i.ShippingStatus).HasConversion<string>();
                entity.Property(i => i.OrderStatus).HasConversion<string>();

            });


            base.OnModelCreating(builder);


        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }


    }
}
