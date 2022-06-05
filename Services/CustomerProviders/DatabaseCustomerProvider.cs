using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.CustomerProviders {
    public class DatabaseCustomerProvider : ICustomerProvider {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseCustomerProvider(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<IEnumerable<Customer>> GetAllCustomers() {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            IEnumerable<CustomerDTO> customerDTOs = await context.Customers.ToListAsync();
            List<Customer> customers = new();
            foreach (var customer in customerDTOs) {
                if (customer != null) {
                    Customer a = new(customer.CustomerID, customer.CustomerName, customer.CustomerSurname, customer.CustomerEmail, customer.CustomerStreet, customer.CustomerCity, customer.CustomerPESEL);
                    customers.Add(a);
                }
            }
            return customers;
        }
    }
}
