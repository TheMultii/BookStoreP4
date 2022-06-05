using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadCustomersCommand : AsyncCommandBase {
        private readonly CustomerListingViewModel _viewModel;
        private readonly CustomerListStore _customerListStore;

        public LoadCustomersCommand(CustomerListingViewModel viewModel, CustomerListStore customerListStore) {
            _viewModel = viewModel;
            _customerListStore = customerListStore;
        }

        public override async Task ExecuteAsync(object parameter) {
            _viewModel.IsLoading = true;
            try {
                await _customerListStore.Load();
                _viewModel.UpdateCustomer(_customerListStore.Customers);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się wyświetlić Klientów", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                _viewModel.IsLoading = false;
            }
        }
    }
}
