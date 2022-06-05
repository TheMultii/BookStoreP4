using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Stores {
    public class BookListStore {
        private readonly BookList _bookList;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Book> _books;

        public IEnumerable<Book> Books => _books;

        public BookListStore(BookList bookList) {
            _bookList = bookList;
            _initializeLazy = new(Initialize);
            _books = new();
        }
        public async Task Load() {
            await _initializeLazy.Value;
        }

        public async Task AddBook(Book newBook) {
            await _bookList.AddBook(newBook);
            _books.Add(newBook);
        }

        private async Task Initialize() {
            IEnumerable<Book> books = await _bookList.GetBooks();
            _books.Clear();
            _books.AddRange(books);
        }
    }
}
