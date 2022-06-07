using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Stores {
    public class OrderListStore {
        private readonly OrderList _orderList;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Order> _orders;

        public IEnumerable<Order> Orders => _orders;

        public OrderListStore(OrderList orderList) {
            _orderList = orderList;
            _initializeLazy = new(Initialize);
            _orders = new();
        }
        public async Task Load() {
            await _initializeLazy.Value;
        }

        public async Task AddOrder(Order newOrder) {
            Order order = await _orderList.AddOrder(newOrder);
            
            if (!_orders.Exists(o => o.OrderID == order.OrderID)) {
                _orders.Add(order);
            } else {
                int index = _orders.FindIndex(o => o.OrderID == order.OrderID);
                _orders[index] = order;
            }
        }

        public async Task DeleteOrder(Order? orderToRemove) {
            Order? order = await _orderList.DeleteOrder(orderToRemove);

            if (order != null) {
                int index = _orders.FindIndex(o => o.OrderID == order.OrderID);
                _orders.RemoveAt(index);
            }
        }

        public async Task AddOrderItem(OrderItem newOrderItem) {
            await _orderList.AddOrderItem(newOrderItem);
            await Initialize();
        }

        private async Task Initialize() {
            IEnumerable<Order> orders = await _orderList.GetOrders();
            _orders.Clear();
            _orders.AddRange(orders);
        }
    }
}
