using BookStoreP4.Models;

namespace BookStoreP4.ViewModels {
    public class EmployeeViewModel : ViewModelBase {
        private readonly Employee _employee;

        public int? EmployeeID => _employee.EmployeeID;
        public string EmployeeName => _employee.EmployeeName;
        public string EmployeeSurname => _employee.EmployeeSurname;
        public string EmployeeEmail => _employee.EmployeeEmail;
        public string EmployeeStreet => _employee.EmployeeStreet;
        public string EmployeeCity => _employee.EmployeeCity;
        public string? EmployeePESEL => _employee.EmployeePESEL;

        public EmployeeViewModel(Employee employee) {
            _employee = employee;
        }
    }
}
