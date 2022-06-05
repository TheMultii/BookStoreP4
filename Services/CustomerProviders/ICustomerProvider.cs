using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.CustomerProviders {
    public interface ICustomerProvider {
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
