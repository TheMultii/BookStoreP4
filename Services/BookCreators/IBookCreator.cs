using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.BookCreators {
    public interface IBookCreator {
        Task CreateBook(Book book);
    }
}
