using Microsoft.EntityFrameworkCore;

namespace BookStoreP4.DBContext {
    public class BookStoreDBContextFactory {
        private readonly string _connectionString;

        public BookStoreDBContextFactory(string connectionString) {
            _connectionString = connectionString;
        }

        public BookStoreDBContext CreateDbContext() {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

            return new BookStoreDBContext(options);
        }
    }
}
