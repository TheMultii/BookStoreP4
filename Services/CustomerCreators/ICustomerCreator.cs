using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.CustomerCreators {
    public interface ICustomerCreator {
        Task<Customer> CreateCustomer(Customer customer);
    }
}
