using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.AuthorProviders {
    public class DatabaseAuthorProvider : IAuthorProvider {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseAuthorProvider(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<IEnumerable<Author>> GetAllAuthors() {
            using (BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext()) {
                IEnumerable<AuthorDTO> authorDTOs = await context.Authors.Include(m => m.Books).ToListAsync();
                List<Author> authors = new();
                foreach (var author in authorDTOs) {
                    if(author != null) {
                        List<Book> books = new();
                        if(author.Books != null) {
                            foreach (var book in author.Books) {
                                Book b = new(book.ISBN, book.Title, book.Description, new List<Author>(), book.Price, book.VAT);
                                books.Add(b);
                            }
                        }
                        Author a = new Author(author.AuthorID, author.AuthorName, author.AuthorSurname, books);
                        authors.Add(a);
                    }
                }
                return authors;
            }
        }
    }
}
