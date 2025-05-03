using LibraryManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API
{
    public class DemoDataContext : DbContext
    {
        public DemoDataContext(DbContextOptions options)
        : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
