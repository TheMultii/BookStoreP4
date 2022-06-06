using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class OrderListingViewModel : ViewModelBase {

        private readonly ObservableCollection<OrderViewModel> _orders;

        public IEnumerable<OrderViewModel> Orders => _orders;

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

        public OrderViewModel? SelectedOrderViewModel = null;

        public ICommand LoadOrdersCommand { get; }
        public ICommand AddOrderCommand { get; }
        public ICommand AddBookCommand { get; }
        public ICommand AddOrderItemCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand ListBooksCommand { get; }
        public ICommand ListAuthorsCommand { get; }
        public ICommand ListCustomersCommand { get; }
        public ICommand ListEmployeesCommand { get; }

        public OrderListingViewModel(OrderListStore orderListStore, NavigationService addOrderNavigationService, NavigationService addBookNavigationService, NavigationService bookListingNavigationService, NavigationService authorListingNavigationService, NavigationService customerListingNavigationService, NavigationService employeeListingNavigationService, NavigationService addOrderItemNavigationService) {
            _orders = new();
            LoadOrdersCommand = new LoadOrdersCommand(this, orderListStore);
            AddOrderCommand = new NavigateCommand(addOrderNavigationService);
            AddBookCommand = new NavigateCommand(addBookNavigationService);
            AddOrderItemCommand = new NavigateCommand(addOrderItemNavigationService);
            ListBooksCommand = new NavigateCommand(bookListingNavigationService);
            ListAuthorsCommand = new NavigateCommand(authorListingNavigationService);
            ListCustomersCommand = new NavigateCommand(customerListingNavigationService);
            ListEmployeesCommand = new NavigateCommand(employeeListingNavigationService);
        }
        
        public static OrderListingViewModel LoadViewModel(OrderListStore orderListStore, NavigationService addOrderNavigationService, NavigationService addBookNavigationService, NavigationService bookListingNavigationService, NavigationService authorListingNavigationService, NavigationService customerListingNavigationService, NavigationService employeeListingNavigationService, NavigationService addOrderItemNavigationService) {
            OrderListingViewModel viewModel = new(orderListStore, addOrderNavigationService, addBookNavigationService, bookListingNavigationService, authorListingNavigationService, customerListingNavigationService, employeeListingNavigationService, addOrderItemNavigationService);
            viewModel.LoadOrdersCommand.Execute(null);
            return viewModel;
        }

        public void UpdateOrders(IEnumerable<Order> orders) {
            _orders.Clear();
            foreach (Order _order in orders) {
                OrderViewModel orderViewModel = new(_order);
                _orders.Add(orderViewModel);
            }
        }
    }
}
