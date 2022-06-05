using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class CustomerListingViewModel : ViewModelBase {

        private readonly ObservableCollection<CustomerViewModel> _customers;

        public IEnumerable<CustomerViewModel> Customers => _customers;

        private bool _isLoading;
        public bool IsLoading {
            get {
                return _isLoading;
            }
            set {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        
        public ICommand LoadCustomersCommand { get; }
        public ICommand ListOrderCommand { get; }
        public ICommand AddCustomerCommand { get; }

        public CustomerListingViewModel(CustomerListStore customerListStore, NavigationService orderViewNavigationService, NavigationService addCustomerNavigationService) {
            _customers = new();
            LoadCustomersCommand = new LoadCustomersCommand(this, customerListStore);
            ListOrderCommand = new NavigateCommand(orderViewNavigationService);
            AddCustomerCommand = new NavigateCommand(addCustomerNavigationService);
        }

        public static CustomerListingViewModel LoadViewModel(CustomerListStore customerListStore, NavigationService orderViewNavigationService, NavigationService addAuthorNavigationService) {
            CustomerListingViewModel viewModel = new(customerListStore, orderViewNavigationService, addAuthorNavigationService);
            viewModel.LoadCustomersCommand.Execute(null);
            return viewModel;
        }
        
        public void UpdateCustomer(IEnumerable<Customer> customers) {
            _customers.Clear();
            foreach (Customer _customer in customers) {
                CustomerViewModel customerViewModel = new(_customer);
                _customers.Add(customerViewModel);
            }
        }
    }
}
