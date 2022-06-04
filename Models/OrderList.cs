using BookStoreP4.Services.OrdersCreators;
using BookStoreP4.Services.OrdersProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class OrderList {
        private readonly IOrderProvider _orderProvider;
        private readonly IOrderCreator _orderCreator;

        public OrderList(IOrderProvider orderProvider, IOrderCreator orderCreator) {
            _orderProvider = orderProvider;
            _orderCreator = orderCreator;
        }

        public async Task<IEnumerable<Order>> GetOrders() => await _orderProvider.GetAllOrders();

        public async Task AddOrder(Order order) {
            await _orderCreator.CreateOrder(order);
        }
    }
}
