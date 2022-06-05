using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.EmployeeCreators {
    public interface IEmployeeCreator {
        Task<Employee> CreateEmployee(Employee employee);
    }
}
