using BookStoreP4.Models;
using BookStoreP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadOrdersCommand : AsyncCommandBase {
        private readonly OrderListingViewModel _viewModel;
        private readonly OrderList _orderList;

        public LoadOrdersCommand(OrderListingViewModel viewModel, OrderList orderList) {
            _viewModel = viewModel;
            _orderList = orderList;
        }

        public override async Task ExecuteAsync(object parameter) {
            try {
                IEnumerable<Order> orders = await _orderList.GetOrders();
                _viewModel.UpdateOrders(orders);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
