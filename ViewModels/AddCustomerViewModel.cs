using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddCustomerViewModel : ViewModelBase {

        private string _customerName;
        public string CustomerName {
            get {
                return _customerName;
            }
            set {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        private string _customerSurname;
        public string CustomerSurname {
            get {
                return _customerSurname;
            }
            set {
                _customerSurname = value;
                OnPropertyChanged(nameof(CustomerSurname));
            }
        }

        private string _customerEmail;
        public string CustomerEmail {
            get {
                return _customerEmail;
            }
            set {
                _customerEmail = value;
                OnPropertyChanged(nameof(CustomerEmail));
            }
        }

        private string _customerStreet;
        public string CustomerStreet {
            get {
                return _customerStreet;
            }
            set {
                _customerStreet = value;
                OnPropertyChanged(nameof(CustomerStreet));
            }
        }

        private string _customerCity;
        public string CustomerCity {
            get {
                return _customerCity;
            }
            set {
                _customerCity = value;
                OnPropertyChanged(nameof(CustomerCity));
            }
        }

        private string _customerPESEL;
        public string CustomerPESEL {
            get {
                return _customerPESEL;
            }
            set {
                _customerPESEL = value;
                OnPropertyChanged(nameof(CustomerPESEL));
            }
        }

        public ICommand SubmitCustomerCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCustomerViewModel(CustomerListStore customerListStore, NavigationService customerViewNavigationService) {
            SubmitCustomerCommand = new AddCustomerCommand(this, customerListStore, customerViewNavigationService);
            CancelCommand = new NavigateCommand(customerViewNavigationService);
        }
    }
}
