using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrdersProviders {
    public class DatabaseOrderProvider : IOrderProvider {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseOrderProvider(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task<IEnumerable<Order>> GetAllOrders() {
            using (BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext()) {
                IEnumerable<OrderDTO> orderDTOs = await context.Orders.Include(m => m.OrderEmployeeID).Include(m => m.OrderCustomerID).ToListAsync();
                List<Order> orders = new();
                foreach (var order in orderDTOs) {
                    if(order != null) {

                        EmployeeDTO? eDTO = order?.OrderEmployeeID;
                        Employee? e = (eDTO != null) ? new(eDTO.EmployeeID, eDTO.EmployeeName, eDTO.EmployeeSurname, eDTO.EmployeeEmail, eDTO.EmployeeStreet, eDTO.EmployeeCity, eDTO?.EmployeePESEL) : null;

                        CustomerDTO? cDTO = order?.OrderCustomerID;
                        Customer? c = (cDTO != null) ? new(cDTO.CustomerID, cDTO.CustomerName, cDTO.CustomerSurname, cDTO.CustomerEmail, cDTO.CustomerStreet, cDTO.CustomerCity, cDTO?.CustomerPESEL) : null;

                        Order o = new(order.OrderID, e, c, order.OrderDate);
                        orders.Add(o);
                    }
                }
                return orders;
            }
        }
    }
}
