using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddCustomerCommand : AsyncCommandBase {
        private readonly AddCustomerViewModel _addCustomerViewModel;
        private readonly CustomerListStore _customerListStore;
        private readonly NavigationService _customerViewNavigationService;

        public AddCustomerCommand(AddCustomerViewModel addCustomerViewModel, CustomerListStore customerListStore, NavigationService customerViewNavigationService) {
            _addCustomerViewModel = addCustomerViewModel;
            _customerListStore = customerListStore;
            _customerViewNavigationService = customerViewNavigationService;
            
            _addCustomerViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addCustomerViewModel?.CustomerName != "" && _addCustomerViewModel?.CustomerName?.Length <= 50 &&
                _addCustomerViewModel?.CustomerSurname != "" && _addCustomerViewModel?.CustomerSurname?.Length <= 55 &&
                _addCustomerViewModel?.CustomerEmail != "" && _addCustomerViewModel?.CustomerPESEL != "" &&
                _addCustomerViewModel?.CustomerPESEL?.Length == 11 && _addCustomerViewModel?.CustomerStreet != "" &&
                _addCustomerViewModel?.CustomerCity != ""
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            try {
                Customer newCustomer = new(int.MaxValue, _addCustomerViewModel.CustomerName, _addCustomerViewModel.CustomerSurname, _addCustomerViewModel.CustomerEmail, _addCustomerViewModel.CustomerStreet, _addCustomerViewModel.CustomerCity, _addCustomerViewModel.CustomerPESEL);

                await _customerListStore.AddCustomer(newCustomer);

                _customerViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać klienta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addCustomerViewModel.CustomerName) || e.PropertyName == nameof(_addCustomerViewModel.CustomerSurname) || e.PropertyName == nameof(_addCustomerViewModel.CustomerEmail) || e.PropertyName == nameof(_addCustomerViewModel.CustomerStreet) || e.PropertyName == nameof(_addCustomerViewModel.CustomerCity) || e.PropertyName == nameof(_addCustomerViewModel.CustomerPESEL)) {
                OnCanExecuteChanged();
            }
        }
    }
}
