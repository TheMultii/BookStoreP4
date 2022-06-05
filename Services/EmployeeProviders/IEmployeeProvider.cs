using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.EmployeeProviders {
    public interface IEmployeeProvider {
        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
