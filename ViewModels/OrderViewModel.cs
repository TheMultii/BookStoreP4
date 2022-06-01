using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreP4.ViewModels {
    public class OrderViewModel : ViewModelBase {
        private readonly Order _order;

        public int OrderID => _order.OrderID;
        public string OrderEmployee => _order.OrderEmployee.ToString();
        public string OrderCustomer => _order.OrderCustomer.ToString();
        public List<OrderItem> OrderItems => _order.OrderItems;
        public DateTime? OrderDate => _order.OrderDate;
        public int OrderItemsCount => _order.OrderItems.Count;
        public int OrderItemsBooksCount => _order.OrderItems.Sum(x => x.Quantity);
        public decimal OrderItemsValue => _order.OrderItems.Sum(x => x.Quantity * x.BookPrice);

        public OrderViewModel(Order order) {
            _order = order;
        }
    }
}
