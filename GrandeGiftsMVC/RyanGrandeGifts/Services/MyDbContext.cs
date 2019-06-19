using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RyanGrandeGifts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RyanGrandeGifts.Services
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }
        public DbSet<Address> TblAddress { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Hamper> TblHamper { get; set; }
        public DbSet<Product> TblProduct { get; set; }
        public DbSet<HamperProduct> TblHamperProducts { get;set; }
        public DbSet<Order> TblOrders { get; set; }
        public DbSet<HamperOrder> TblHamperOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
            
        {
            base.OnModelCreating(builder);

            builder.Entity<HamperProduct>()
                .HasKey(hp => new { hp.HamperId, hp.ProductId });
            builder.Entity<HamperProduct>()
                .HasOne(hp => hp.Hamper)
                .WithMany(h => h.HamperProducts)
                .HasForeignKey(hp => hp.HamperId);
            builder.Entity<HamperProduct>()
               .HasOne(hp => hp.Product)
               .WithMany(p => p.HamperProducts)
               .HasForeignKey(hp => hp.ProductId);
        }
    }
}
