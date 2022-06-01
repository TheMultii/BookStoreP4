using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddBookCommand : AsyncCommandBase {
        private readonly AddBookViewModel _addBookViewModel;
        private readonly OrderList _orderList;
        private readonly NavigationService _orderViewNavigationService;

        public AddBookCommand(AddBookViewModel addBookViewModel, OrderList orderList, NavigationService orderViewNavigationService) {
            _addBookViewModel = addBookViewModel;
            _orderList = orderList;
            _orderViewNavigationService = orderViewNavigationService;

            _addBookViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                //_addBookViewModel.OrderEmployeeID != 0 &&
                //_addBookViewModel.OrderCustomerID != 0 &&
                //_addBookViewModel.OrderDateTime.Year >= DateTime.Now.Year - 1 &&
                //_addBookViewModel.OrderDateTime.Year <= DateTime.Now.Year + 1
                true //TODO
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {

            try {

                //Employee e = new(_addOrderViewModel.OrderEmployeeID.ToString(), "Kowalski", "mail@mail.pl", "Ulica 1", "Bielsko-Biała");

                //Customer p = new(_addOrderViewModel.OrderCustomerID.ToString(), "Kowalski", "mail@mail.pl", "Ulica 1", "Bielsko-Biała");

                //Order newOrder = new(
                //    1,
                //    e,
                //    p,
                //    _addOrderViewModel.OrderDateTime
                //);

                //await _orderList.AddOrder(newOrder);
                //TODO

                _orderViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            //if (e.PropertyName == nameof(_addBookViewModel.) || e.PropertyName == nameof(_addBookViewModel.OrderCustomerID) || e.PropertyName == nameof(_addOrderViewModel.OrderDateTime)) {
            OnCanExecuteChanged();
            //}
        }
    }
}
