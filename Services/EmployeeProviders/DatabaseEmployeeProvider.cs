using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.EmployeeProviders {
    public class DatabaseEmployeeProvider : IEmployeeProvider {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseEmployeeProvider(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<IEnumerable<Employee>> GetAllEmployees() {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            IEnumerable<EmployeeDTO> employeeDTOs = await context.Employees.ToListAsync();
            List<Employee> employees = new();
            foreach (var employee in employeeDTOs) {
                if (employee != null) {
                    Employee a = new(employee.EmployeeID, employee.EmployeeName, employee.EmployeeSurname, employee.EmployeeEmail, employee.EmployeeStreet, employee.EmployeeCity, employee.EmployeePESEL);
                    employees.Add(a);
                }
            }
            return employees;
        }
    }
}
