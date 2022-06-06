using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddEmployeeViewModel : ViewModelBase {

        private string _employeeName;
        public string EmployeeName {
            get {
                return _employeeName;
            }
            set {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private string _employeeSurname;
        public string EmployeeSurname {
            get {
                return _employeeSurname;
            }
            set {
                _employeeSurname = value;
                OnPropertyChanged(nameof(EmployeeSurname));
            }
        }

        private string _employeeEmail;
        public string EmployeeEmail {
            get {
                return _employeeEmail;
            }
            set {
                _employeeEmail = value;
                OnPropertyChanged(nameof(EmployeeEmail));
            }
        }

        private string _employeeStreet;
        public string EmployeeStreet {
            get {
                return _employeeStreet;
            }
            set {
                _employeeStreet = value;
                OnPropertyChanged(nameof(EmployeeStreet));
            }
        }

        private string _employeeCity;
        public string EmployeeCity {
            get {
                return _employeeCity;
            }
            set {
                _employeeCity = value;
                OnPropertyChanged(nameof(EmployeeCity));
            }
        }

        private string _employeePESEL;
        public string EmployeePESEL {
            get {
                return _employeePESEL;
            }
            set {
                _employeePESEL = value;
                OnPropertyChanged(nameof(EmployeePESEL));
            }
        }

        public ICommand SubmitEmployeeCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEmployeeViewModel(EmployeeListStore employeeListStore, NavigationService employeeViewNavigationService) {
            SubmitEmployeeCommand = new AddEmployeeCommand(this, employeeListStore, employeeViewNavigationService);
            CancelCommand = new NavigateCommand(employeeViewNavigationService);
        }
    }
}
