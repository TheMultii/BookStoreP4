using BookStoreP4.Services.BookCreators;
using BookStoreP4.Services.BooksProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class BookList {
        private readonly IBookProvider _bookProvider;
        private readonly IBookCreator _bookCreator;

        public BookList(IBookProvider bookProvider, IBookCreator bookCreator) {
            _bookProvider = bookProvider;
            _bookCreator = bookCreator;
        }

        public async Task<IEnumerable<Book>> GetBooks() => await _bookProvider.GetAllBooks();

        public async Task AddBook(Book book) {
            await _bookCreator.CreateBook(book);
        }
    }
}
