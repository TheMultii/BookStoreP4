using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class OrderListingViewModel : ViewModelBase {

        private readonly ObservableCollection<OrderViewModel> _orders;

        public IEnumerable<OrderViewModel> Orders => _orders;

        public ICommand LoadOrdersCommand { get; }
        public ICommand AddOrderCommand { get; }
        public ICommand AddBookCommand { get; }

        public OrderListingViewModel(OrderList orderList, NavigationService addOrderNavigationService, NavigationService addBookNavigationService) {
            _orders = new();
            LoadOrdersCommand = new LoadOrdersCommand(this, orderList);
            AddOrderCommand = new NavigateCommand(addOrderNavigationService);
            AddBookCommand = new NavigateCommand(addBookNavigationService);
        }

        public static OrderListingViewModel LoadViewModel(OrderList orderList, NavigationService addOrderNavigationService, NavigationService addBookNavigationService) {
            OrderListingViewModel viewModel = new(orderList, addOrderNavigationService, addBookNavigationService);
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
