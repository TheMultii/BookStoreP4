using BookStoreP4.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookStoreP4.DBContext {
    public class BookStoreDBContext : DbContext {
        public BookStoreDBContext(DbContextOptions options) : base(options) { }

        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<BookDTO> Books { get; set; }
        public DbSet<AuthorDTO> Authors { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderItemDTO> OrderItems { get; set; }
    }
}
