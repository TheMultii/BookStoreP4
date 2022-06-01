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
        private readonly OrderList _orderList;

        public IEnumerable<OrderViewModel> Orders => _orders;

        public ICommand AddOrderCommand { get; }

        public OrderListingViewModel(OrderList orderList, NavigationService addOrderNavigationService) {
            _orders = new();
            _orderList = orderList;
            AddOrderCommand = new NavigateCommand(addOrderNavigationService);

            UpdateOrders();
        }

        private void UpdateOrders() {
            _orders.Clear();
            foreach (Order _order in _orderList.GetOrders()) {
                OrderViewModel orderViewModel = new(_order);
                _orders.Add(orderViewModel);
            }
        }
    }
}
