using Bogus;
using Bogus.Extensions.Poland;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddOrderItemCommand : AsyncCommandBase {
        private readonly AddOrderItemViewModel _addOrderItemViewModel;
        private readonly OrderListStore _orderListStore;
        private readonly NavigationService _orderViewNavigationService;

        public AddOrderItemCommand(AddOrderItemViewModel addOrderItemViewModel, OrderListStore orderListStore, NavigationService orderViewNavigationService) {
            _addOrderItemViewModel = addOrderItemViewModel;
            _orderListStore = orderListStore;
            _orderViewNavigationService = orderViewNavigationService;

            _addOrderItemViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addOrderItemViewModel?.OrderItemBook?.Length == 13 &&
                _addOrderItemViewModel?.OrderItemOrder > 0 &&
                _addOrderItemViewModel?.OrderItemQuantity > 0
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            
            try {
                Book book = new(_addOrderItemViewModel.OrderItemBook, "", "", new List<Author>(), 0);
                Order order = new(_addOrderItemViewModel.OrderItemOrder, new(int.MaxValue, "", "", "", "", ""), new(int.MaxValue, "", "", "", "", ""));
                OrderItem oi = new(int.MaxValue, order, book, _addOrderItemViewModel.OrderItemQuantity);
                
                await _orderListStore.AddOrderItem(oi);

                _orderViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać pozycji zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addOrderItemViewModel.OrderItemBook) || e.PropertyName == nameof(_addOrderItemViewModel.OrderItemOrder) || e.PropertyName == nameof(_addOrderItemViewModel.OrderItemQuantity)) {
                OnCanExecuteChanged();
            }
        }
    }
}
