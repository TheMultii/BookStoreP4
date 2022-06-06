using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Stores {
    public class CustomerListStore {
        private readonly CustomerList _customerList;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Customer> _customers;

        public IEnumerable<Customer> Customers => _customers;

        public CustomerListStore(CustomerList customerList) {
            _customerList = customerList;
            _initializeLazy = new(Initialize);
            _customers = new();
        }
        public async Task Load() {
            await _initializeLazy.Value;
        }
        
        public async Task AddCustomer(Customer newCustomer) {
            Customer added = await _customerList.AddCustomer(newCustomer);
            _customers.Add(added);
        }

        private async Task Initialize() {
            IEnumerable<Customer> customers = await _customerList.GetCustomers();
            _customers.Clear();
            _customers.AddRange(customers);
        }
    }
}
