using KairosTest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace KairosTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Buku> Buku { get; set; }
        public DbSet<SewaBuku> SewaBuku { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buku>(eb =>
            {
                eb.Property(b => b.HargaSewa).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<SewaBuku>(a =>
            {
                a.Property(e => e.JumlahHari)
                    .HasComputedColumnSql("DATEDIFF(day, [MulaiSewa], [SelesaiSewa])", stored: true);
                //a.HasIndex(p => p.JumlahHari);
            });

            modelBuilder.Entity<IdentityRole>(x =>
            {
                x.HasData(
                    new IdentityRole { Id = "539491f8-b120-43e3-ac44-9e79661058d0", Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                    new IdentityRole { Id = "0158ccb0-068b-450f-b1f8-0ad09cd38834", Name = "Penyewa", NormalizedName = "PENYEWA".ToUpper() }
                );
            });

            //modelBuilder.Entity<IdentityUser>(x =>
            //{
            //    x.HasData(
            //        new IdentityUser { Id = "0c0a00da-54c7-4c04-973c-83cb41e30e6a", UserName = "admin", Email = "admin@email.com" },
            //        new IdentityUser { Id = "07d188cf-1228-4a0e-a101-410f13b2e588", UserName = "user1", Email = "user1@email.com" }
            //    );
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
