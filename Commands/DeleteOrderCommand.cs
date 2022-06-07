using BookStoreP4.Models;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {

    public class DeleteOrderCommand : AsyncCommandBase {
        private readonly OrderListingViewModel _orderListingViewModel;
        private readonly OrderListStore _orderListStore;

        public DeleteOrderCommand(OrderListingViewModel orderListingViewModel, OrderListStore orderListStore) {
            _orderListingViewModel = orderListingViewModel;
            _orderListStore = orderListStore;
        }

        public override bool CanExecute(object parameter) {
            return true;
        }

        public override async Task ExecuteAsync(object? parameter) {
            
            try {
                OrderViewModel? orderViewModel = (OrderViewModel?)Application.Current.Properties["SelectedOrder"];
                if (orderViewModel == null) {
                    MessageBox.Show("Najpierw trzeba zaznaczyć zamówienie z listy zamówień", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Order order = new(orderViewModel.OrderID, new(int.MaxValue, "", "", "", "", ""), new(int.MaxValue, "", "", "", "", ""));

                await _orderListStore.DeleteOrder(order);
                Application.Current.Properties["SelectedOrder"] = null;
                _orderListingViewModel.LoadOrdersCommand.Execute(null);

            } catch (Exception ex) {
                Application.Current.Properties["SelectedOrder"] = null;
                MessageBox.Show("Wystąpił błąd - nie udało się usunąć zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
