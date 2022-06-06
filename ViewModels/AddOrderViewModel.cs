using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddOrderViewModel : ViewModelBase {

        private int _orderID;
        public int OrderID {
            get {
                return _orderID;
            }
            set {
                _orderID = value;
                OnPropertyChanged(nameof(OrderID));
            }
        }

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
        public ICommand LoadCustomersCommand { get; }

        public AddOrderViewModel(OrderListStore orderListStore, NavigationService orderViewNavigationService) {
            SubmitCommand = new AddOrderCommand(this, orderListStore, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
            try {
                OrderViewModel? selectedOrderViewModel = (OrderViewModel)System.Windows.Application.Current.Properties["SelectedOrder"];
                if (selectedOrderViewModel != null) {
                    OrderID = selectedOrderViewModel.OrderID;
                    OrderCustomerID = selectedOrderViewModel.OrderCustomerObject.CustomerID;
                    OrderEmployeeID = selectedOrderViewModel.OrderEmployeeObject.EmployeeID;
                    OrderDateTime = selectedOrderViewModel.OrderDate ?? DateTime.Now;
                    App.Current.Properties["SelectedOrder"] = null;
                }
            } catch (Exception e) {
                //
            }
        }
    }
}
