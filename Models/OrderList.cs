using BookStoreP4.Services.OrderItemCreators;
using BookStoreP4.Services.OrdersCreators;
using BookStoreP4.Services.OrdersProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class OrderList {
        private readonly IOrderProvider _orderProvider;
        private readonly IOrderCreator _orderCreator;
        private readonly IOrderItemCreator _orderItemCreator;

        public OrderList(IOrderProvider orderProvider, IOrderCreator orderCreator, IOrderItemCreator orderItemCreator) {
            _orderProvider = orderProvider;
            _orderCreator = orderCreator;
            _orderItemCreator = orderItemCreator;
        }

        public async Task<IEnumerable<Order>> GetOrders() => await _orderProvider.GetAllOrders();

        public async Task<Order> AddOrder(Order order) {
            return await _orderCreator.CreateOrder(order);
        }
        public async Task<Order?> DeleteOrder(Order? order) {
            return await _orderCreator.DeleteOrder(order);
        }

        public async Task AddOrderItem(OrderItem orderItem) {
            await _orderItemCreator.CreateOrderItem(orderItem);
        }
    }
}
