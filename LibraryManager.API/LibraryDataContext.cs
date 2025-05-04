using LibraryManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions options)
        : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
    }
}
