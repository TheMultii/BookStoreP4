using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.BooksProviders {
    public interface IBookProvider {
        Task<IEnumerable<Book>> GetAllBooks();
    }
}
