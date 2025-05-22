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
               

                entity.HasMany(i => i.ProductSizes).WithOne(i => i.Product).HasForeignKey(i => i.productId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Order>(entity =>
            {
                //entity.Property(i => i.Size).HasConversion<string>();
                //entity.Property(i => i.ShippingStatus).HasConversion<string>();
                entity.Property(i => i.OrderStatus).HasConversion<string>();
              


                
            });

           

            builder.Entity<MasterOrder>(entity =>
            {
                entity.HasMany(i => i.Orders).WithOne(i => i.MasterOrder).HasForeignKey(i => i.masterOrderId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(i => i.User).WithMany(i => i.MasterOrders).HasForeignKey(i => i.userId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(i => i.DeliveryAdress).WithOne(i => i.MasterOrder).HasForeignKey<DeliveryAdress>(i => i.masterOrderId).OnDelete(DeleteBehavior.NoAction);

            });

           


            base.OnModelCreating(builder);


        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductSizes> ProductSizes { get; set; }


        public DbSet<DeliveryAdress> OrderDeliveryAdresses { get; set;}

        public DbSet<MasterOrder> MasterOrders { get; set; }

    }
}
