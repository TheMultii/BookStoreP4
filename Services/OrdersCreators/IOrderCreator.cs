using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrdersCreators {
    public interface IOrderCreator {
        Task CreateOrder(Order order);
    }
}
