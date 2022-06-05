using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddOrderViewModel : ViewModelBase {

        private int _customerID;
        public int OrderCustomerID {
            get {
                return _customerID;
            }
            set {
                _customerID = value;
                OnPropertyChanged(nameof(OrderCustomerID));
            }
        }

        private int _employeeID;
        public int OrderEmployeeID {
            get {
                return _employeeID;
            }
            set {
                _employeeID = value;
                OnPropertyChanged(nameof(OrderEmployeeID));
            }
        }

        private DateTime _datetime = DateTime.Now;
        public DateTime OrderDateTime {
            get {
                return _datetime;
            }
            set {
                _datetime = value;
                OnPropertyChanged(nameof(OrderDateTime));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddOrderViewModel(OrderListStore orderListStore, NavigationService orderViewNavigationService) {
            SubmitCommand = new AddOrderCommand(this, orderListStore, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
        }
    }
}
