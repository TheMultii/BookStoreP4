using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddEmployeeCommand : AsyncCommandBase {
        private readonly AddEmployeeViewModel _addEmployeeViewModel;
        private readonly EmployeeListStore _employeeListStore;
        private readonly NavigationService _employeeViewNavigationService;

        public AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, EmployeeListStore employeeListStore, NavigationService employeeViewNavigationService) {
            _addEmployeeViewModel = addEmployeeViewModel;
            _employeeListStore = employeeListStore;
            _employeeViewNavigationService = employeeViewNavigationService;
            
            _addEmployeeViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addEmployeeViewModel?.EmployeeName != "" && _addEmployeeViewModel?.EmployeeName?.Length <= 50 &&
                _addEmployeeViewModel?.EmployeeSurname != "" && _addEmployeeViewModel?.EmployeeSurname?.Length <= 55 &&
                _addEmployeeViewModel?.EmployeeEmail != "" && _addEmployeeViewModel?.EmployeePESEL != "" &&
                _addEmployeeViewModel?.EmployeePESEL?.Length == 11 && _addEmployeeViewModel?.EmployeeStreet != "" &&
                _addEmployeeViewModel?.EmployeeCity != ""
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            try {
                Employee newEmployee = new(int.MaxValue, _addEmployeeViewModel.EmployeeName, _addEmployeeViewModel.EmployeeSurname, _addEmployeeViewModel.EmployeeEmail, _addEmployeeViewModel.EmployeeStreet, _addEmployeeViewModel.EmployeeCity, _addEmployeeViewModel.EmployeePESEL);

                await _employeeListStore.AddEmployee(newEmployee);

                _employeeViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać pracownika", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addEmployeeViewModel.EmployeeName) || e.PropertyName == nameof(_addEmployeeViewModel.EmployeeSurname) || e.PropertyName == nameof(_addEmployeeViewModel.EmployeeEmail) || e.PropertyName == nameof(_addEmployeeViewModel.EmployeeStreet) || e.PropertyName == nameof(_addEmployeeViewModel.EmployeeCity) || e.PropertyName == nameof(_addEmployeeViewModel.EmployeePESEL)) {
                OnCanExecuteChanged();
            }
        }
    }
}
