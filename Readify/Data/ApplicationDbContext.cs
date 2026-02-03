using Readify.Models;
using Readify.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Readify.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("tblCategory");

                entity.HasKey(c => c.intCategoryId);

                entity.Property(c => c.strSubject)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.intVolumeNumber)
                      .IsRequired();

                entity.HasMany(c => c.Books)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.intCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("tblBook");

                entity.HasKey(c => c.intBookId);

                entity.Property(c => c.strTitle)
                      .IsRequired();

                entity.Property(b => b.dclPrice)
                      .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("tblRental");

                entity.HasKey(r => r.intRentalId);

                entity.Property(r => r.dclTotalPrice)
                .HasPrecision(18, 2);

                entity.HasOne(r => r.Book)
                      .WithMany(b=> b.Rentals )
                      .HasForeignKey(r => r.intBookId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
