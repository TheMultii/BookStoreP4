using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddOrderItemViewModel : ViewModelBase {

        private string _orderItemBook;
        public string OrderItemBook {
            get {
                return _orderItemBook;
            }
            set {
                _orderItemBook = value;
                OnPropertyChanged(nameof(OrderItemBook));
            }
        }
        
        private int _orderItemOrder;
        public int OrderItemOrder {
            get {
                return _orderItemOrder;
            }
            set {
                if(value > 0) {
                    _orderItemOrder = value;
                    OnPropertyChanged(nameof(OrderItemOrder));
                }
            }
        }

        private int _orderItemQuantity;
        public int OrderItemQuantity {
            get {
                return _orderItemQuantity;
            }
            set {
                if (value > 0) {
                    _orderItemQuantity = value;
                    OnPropertyChanged(nameof(OrderItemQuantity));
                }
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddOrderItemViewModel(OrderListStore orderListStore, NavigationService orderViewNavigationService) {
            SubmitCommand = new AddOrderItemCommand(this, orderListStore, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
        }
    }
}
