using System;
using System.Collections.Generic;

namespace BookStoreP4.Models {
    public class Order {
        public int OrderID { get; }
        public Employee OrderEmployee { get; }
        public Customer OrderCustomer { get; }
        public List<OrderItem> OrderItems { get; }
        public DateTime OrderDate { get; }

        public Order(int orderID, Employee employee, Customer customer, DateTime orderDate) {
            OrderID = orderID;
            OrderEmployee = employee;
            OrderCustomer = customer;
            OrderItems = new();
            OrderDate = orderDate;
        }
        public Order(int orderID, Employee employee, Customer customer) {
            OrderID = orderID;
            OrderEmployee = employee;
            OrderCustomer = customer;
            OrderItems = new();
            OrderDate = DateTime.Now;
        }
    }
}
