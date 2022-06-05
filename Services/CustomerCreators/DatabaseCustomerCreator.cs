using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Services.CustomerCreators {
    public class DatabaseCustomerCreator : ICustomerCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseCustomerCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<Customer> CreateCustomer(Customer customer) {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            using var transaction = context.Database.BeginTransaction();
            CustomerDTO customerDTO = ToCustomerDTO(customer);

            context.Customers.Add(customerDTO);
            await context.SaveChangesAsync();

            var result = context.Customers.AsNoTracking().SingleOrDefault(b => b.CustomerName == customer.CustomerName && b.CustomerSurname == customer.CustomerSurname);

            Customer customerReturn = new(result != null ? result.CustomerID : int.MaxValue, result?.CustomerName ?? customer.CustomerName, result?.CustomerSurname ?? customer.CustomerSurname, result?.CustomerEmail ?? customer.CustomerEmail, result?.CustomerStreet ?? customer.CustomerStreet, result?.CustomerCity ?? customer.CustomerCity, result?.CustomerPESEL ?? customer.CustomerPESEL);
            
            transaction.Commit();
            return result != null ? customerReturn : customer;
        }

        private CustomerDTO ToCustomerDTO(Customer customer) {
            CustomerDTO customerDTO = new();
            customerDTO.CustomerName = customer.CustomerName;
            customerDTO.CustomerSurname = customer.CustomerSurname;
            customerDTO.CustomerEmail = customer.CustomerEmail;
            customerDTO.CustomerStreet = customer.CustomerStreet;
            customerDTO.CustomerCity = customer.CustomerCity;
            customerDTO.CustomerPESEL = customer.CustomerPESEL;
            return customerDTO;
        }
    }
}
