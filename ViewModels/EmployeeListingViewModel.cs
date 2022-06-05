using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class EmployeeListingViewModel : ViewModelBase {

        private readonly ObservableCollection<EmployeeViewModel> _employees;

        public IEnumerable<EmployeeViewModel> Employees => _employees;

        private bool _isLoading;
        public bool IsLoading {
            get {
                return _isLoading;
            }
            set {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        
        public ICommand LoadEmployeesCommand { get; }
        public ICommand ListOrderCommand { get; }
        public ICommand AddEmployeeCommand { get; }

        public EmployeeListingViewModel(EmployeeListStore employeeListStore, NavigationService orderViewNavigationService, NavigationService addEmployeeNavigationService) {
            _employees = new();
            LoadEmployeesCommand = new LoadEmployeesCommand(this, employeeListStore);
            ListOrderCommand = new NavigateCommand(orderViewNavigationService);
            AddEmployeeCommand = new NavigateCommand(addEmployeeNavigationService);
        }

        public static EmployeeListingViewModel LoadViewModel(EmployeeListStore employeeListStore, NavigationService orderViewNavigationService, NavigationService addAuthorNavigationService) {
            EmployeeListingViewModel viewModel = new(employeeListStore, orderViewNavigationService, addAuthorNavigationService);
            viewModel.LoadEmployeesCommand.Execute(null);
            return viewModel;
        }
        
        public void UpdateEmployee(IEnumerable<Employee> employees) {
            _employees.Clear();
            foreach (Employee _employee in employees) {
                EmployeeViewModel employeeViewModel = new(_employee);
                _employees.Add(employeeViewModel);
            }
        }
    }
}
