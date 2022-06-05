using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Stores {
    public class EmployeeListStore {
        private readonly EmployeeList _employeeList;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Employee> _employees;

        public IEnumerable<Employee> Employees => _employees;

        public EmployeeListStore(EmployeeList employeeList) {
            _employeeList = employeeList;
            _initializeLazy = new(Initialize);
            _employees = new();
        }
        public async Task Load() {
            await _initializeLazy.Value;
        }
        
        public async Task AddAuthor(Employee newEmployee) {
            Employee added = await _employeeList.AddEmployee(newEmployee);
            _employees.Add(added);
        }

        private async Task Initialize() {
            IEnumerable<Employee> employees = await _employeeList.GetEmployees();
            _employees.Clear();
            _employees.AddRange(employees);
        }
    }
}
