using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrderItemCreators {
    public interface IOrderItemCreator {
        Task CreateOrderItem(OrderItem orderItem);
    }
}
