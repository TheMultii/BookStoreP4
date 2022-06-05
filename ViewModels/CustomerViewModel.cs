using BookStoreP4.Models;

namespace BookStoreP4.ViewModels {
    public class CustomerViewModel : ViewModelBase {
        private readonly Customer _customer;

        public int? CustomerID => _customer.CustomerID;
        public string CustomerName => _customer.CustomerName;
        public string CustomerSurname => _customer.CustomerSurname;
        public string CustomerEmail => _customer.CustomerEmail;
        public string CustomerStreet => _customer.CustomerStreet;
        public string CustomerCity => _customer.CustomerCity;
        public string? CustomerPESEL => _customer.CustomerPESEL;

        public CustomerViewModel(Customer customer) {
            _customer = customer;
        }
    }
}
