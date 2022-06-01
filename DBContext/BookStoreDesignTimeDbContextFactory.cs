using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStoreP4.DBContext {
    public class BookStoreDesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookStoreDBContext> {
        public BookStoreDBContext CreateDbContext(string[] args) {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer("Server=MARCEL-PC\\SQLEXPRESS;Database=BookStoreP4;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

            return new BookStoreDBContext(options);
        }
    }
}
