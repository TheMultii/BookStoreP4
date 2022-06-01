using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrdersProviders {
    public interface IOrderProvider {
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
