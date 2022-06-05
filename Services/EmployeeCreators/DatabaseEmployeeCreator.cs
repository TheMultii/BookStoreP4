using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Services.EmployeeCreators {
    public class DatabaseEmployeeCreator : IEmployeeCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseEmployeeCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<Employee> CreateEmployee(Employee employee) {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            using var transaction = context.Database.BeginTransaction();
            EmployeeDTO employeeDTO = ToEmployeeDTO(employee);

            context.Employees.Add(employeeDTO);
            await context.SaveChangesAsync();

            var result = context.Employees.AsNoTracking().SingleOrDefault(b => b.EmployeeName == employee.EmployeeName && b.EmployeeSurname == employee.EmployeeSurname);

            Employee employeeReturn = new(result != null ? result.EmployeeID : int.MaxValue, result?.EmployeeName ?? employee.EmployeeName, result?.EmployeeSurname ?? employee.EmployeeSurname, result?.EmployeeEmail ?? employee.EmployeeEmail, result?.EmployeeStreet ?? employee.EmployeeStreet, result?.EmployeeCity ?? employee.EmployeeCity, result?.EmployeePESEL ?? employee.EmployeePESEL);
            
            transaction.Commit();
            return result != null ? employeeReturn : employee;
        }

        private EmployeeDTO ToEmployeeDTO(Employee employee) {
            EmployeeDTO employeeDTO = new();
            employeeDTO.EmployeeName = employee.EmployeeName;
            employeeDTO.EmployeeSurname = employee.EmployeeSurname;
            employeeDTO.EmployeeEmail = employee.EmployeeEmail;
            employeeDTO.EmployeeStreet = employee.EmployeeStreet;
            employeeDTO.EmployeeCity = employee.EmployeeCity;
            employeeDTO.EmployeePESEL = employee.EmployeePESEL;
            return employeeDTO;
        }
    }
}
