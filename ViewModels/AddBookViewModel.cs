using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddBookViewModel : ViewModelBase {

        private int _orderOrder;
        public int OrderOrder {
            get {
                return _orderOrder;
            }
            set {
                _orderOrder = value;
                OnPropertyChanged(nameof(OrderOrder));
            }
        }

        private string _orderBookTitle;
        public string OrderBookTitle {
            get {
                return _orderBookTitle;
            }
            set {
                _orderBookTitle = value;
                OnPropertyChanged(nameof(OrderBookTitle));
            }
        }

        private string _orderBookDescription;
        public string OrderBookDescription {
            get {
                return _orderBookDescription;
            }
            set {
                _orderBookDescription = value;
                OnPropertyChanged(nameof(OrderBookDescription));
            }
        }

        private int _orderBookQuantity;
        public int OrderBookQuantity {
            get {
                return _orderBookQuantity;
            }
            set {
                if(value > 0) {
                    _orderBookQuantity = value;
                    OnPropertyChanged(nameof(OrderBookQuantity));
                }
            }
        }

        private string _orderAuthorName;
        public string OrderAuthorName {
            get {
                return _orderAuthorName;
            }
            set {
                _orderAuthorName = value;
                OnPropertyChanged(nameof(OrderAuthorName));
            }
        }

        private string _orderAuthorSurname;
        public string OrderAuthorSurname {
            get {
                return _orderAuthorSurname;
            }
            set {
                _orderAuthorSurname = value;
                OnPropertyChanged(nameof(OrderAuthorSurname));
            }
        }

        private float _orderBookPrice;
        public float OrderBookPrice {
            get {
                return _orderBookPrice;
            }
            set {
                if(value > 0) {
                    _orderBookPrice = value;
                    OnPropertyChanged(nameof(OrderBookPrice));
                }
            }
        }

        private float _orderBookVAT;
        public float OrderBookVAT {
            get {
                return _orderBookVAT;
            }
            set {
                if(value >= 0 && value <= 1) {
                    _orderBookVAT = value;
                    OnPropertyChanged(nameof(OrderBookVAT));
                }
            }
        }

        public ICommand SubmitBookCommand { get; }
        public ICommand CancelCommand { get; }

        public AddBookViewModel(OrderList orderList, NavigationService orderViewNavigationService) {
            SubmitBookCommand = new AddBookCommand(this, orderList, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
        }
    }
}
