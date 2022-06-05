using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.BooksProviders {
    public class DatabaseBookProvider : IBookProvider {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseBookProvider(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task<IEnumerable<Book>> GetAllBooks() {
            using (BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext()) {
                IEnumerable<BookDTO> bookDTOs = await context.Books.Include(m => m.Authors).ToListAsync();
                List<Book> books = new();
                foreach (var book in bookDTOs) {
                    if(book != null) {
                        List<Author> authors = new();
                        if (book.Authors != null) {
                            foreach (AuthorDTO author in book.Authors) {
                                authors.Add(new(author.AuthorID, author.AuthorName, author.AuthorSurname));
                            }
                        }

                        Book b = new(book.ISBN, book.Title, book.Description, authors, book.Price, book?.VAT);
                        books.Add(b);
                    }
                }
                return books;
            }
        }
    }
}
