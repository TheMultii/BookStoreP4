using System.Collections.Generic;
using System.Linq;

namespace BookStoreP4.Models {
    public class OrderList {
        private readonly List<Order> _orders;

        public OrderList() {
            _orders = new();
        }

        public IEnumerable<Order> GetOrdersForEmployee(string name, string surname) {
            return _orders.Where(o => o.OrderEmployee.EmployeeName?.ToLower() == name?.ToLower() && o.OrderEmployee.EmployeeSurname?.ToLower() == surname?.ToLower());
        }

        public IEnumerable<Order> GetOrdersForCustomer(string name, string surname) {
            return _orders.Where(o => o.OrderCustomer.CustomerName?.ToLower() == name?.ToLower() && o.OrderCustomer.CustomerSurname?.ToLower() == surname?.ToLower());
        }

        public IEnumerable<Order> GetOrders() => _orders;

        public void AddOrder(Order order) {
            _orders.Add(order);
        }
    }
}
