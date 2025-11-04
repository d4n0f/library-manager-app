using LibraryManager.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions<LibraryDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Könyv törléseének korlátozása, ha tartozik hozzá kölcsönzés
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Book)
                .WithMany()
                .HasForeignKey(r => r.InventoryNumber)
                .OnDelete(DeleteBehavior.Restrict);

            // Olvasó törléseének korlátozása, ha tartozik hozzá kölcsönzés
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Reader)
                .WithMany()
                .HasForeignKey(r => r.ReaderNumber)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
