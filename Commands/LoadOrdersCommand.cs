using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadOrdersCommand : AsyncCommandBase {
        private readonly OrderListingViewModel _viewModel;
        private readonly OrderListStore _orderListStore;

        public LoadOrdersCommand(OrderListingViewModel viewModel, OrderListStore orderListStore) {
            _viewModel = viewModel;
            _orderListStore = orderListStore;
        }

        public override async Task ExecuteAsync(object parameter) {
            _viewModel.IsLoading = true;
            try {
                await _orderListStore.Load();
                
                //await Task.Delay(TimeSpan.FromSeconds(2));
                
                _viewModel.UpdateOrders(_orderListStore.Orders);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się wyświetlić zamówień", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                _viewModel.IsLoading = false;
            }
        }
    }
}
