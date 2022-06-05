using BookStoreP4.Services.EmployeeCreators;
using BookStoreP4.Services.EmployeeProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class EmployeeList {
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IEmployeeCreator _employeeCreator;

        public EmployeeList(IEmployeeProvider employeeProvider, IEmployeeCreator employeeCreator) {
            _employeeProvider = employeeProvider;
            _employeeCreator = employeeCreator;
        }
        
        public async Task<IEnumerable<Employee>> GetEmployees() => await _employeeProvider.GetAllEmployees();

        public async Task<Employee> AddEmployee(Employee employee) {
            return await _employeeCreator.CreateEmployee(employee);
        }
    }
}
