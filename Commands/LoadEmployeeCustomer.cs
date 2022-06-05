using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadEmployeesCommand : AsyncCommandBase {
        private readonly EmployeeListingViewModel _viewModel;
        private readonly EmployeeListStore _employeeListStore;

        public LoadEmployeesCommand(EmployeeListingViewModel viewModel, EmployeeListStore employeeListStore) {
            _viewModel = viewModel;
            _employeeListStore = employeeListStore;
        }

        public override async Task ExecuteAsync(object parameter) {
            _viewModel.IsLoading = true;
            try {
                await _employeeListStore.Load();
                _viewModel.UpdateEmployee(_employeeListStore.Employees);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się wyświetlić Pracowników", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                _viewModel.IsLoading = false;
            }
        }
    }
}
