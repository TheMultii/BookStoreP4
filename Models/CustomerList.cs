using BookStoreP4.Services.CustomerCreators;
using BookStoreP4.Services.CustomerProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class CustomerList {
        private readonly ICustomerProvider _customerProvider;
        private readonly ICustomerCreator _customerCreator;

        public CustomerList(ICustomerProvider authorProvider, ICustomerCreator authorCreator) {
            _customerProvider = authorProvider;
            _customerCreator = authorCreator;
        }
        
        public async Task<IEnumerable<Customer>> GetCustomers() => await _customerProvider.GetAllCustomers();

        public async Task<Customer> AddCustomer(Customer customer) {
            return await _customerCreator.CreateCustomer(customer);
        }
    }
}
