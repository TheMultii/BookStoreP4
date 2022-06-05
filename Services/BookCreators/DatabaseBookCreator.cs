using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Services.BookCreators {
    public class DatabaseBookCreator : IBookCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseBookCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task CreateBook(Book book) {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            using var transaction = context.Database.BeginTransaction();
            BookDTO bookDTO = ToBookDTO(book);
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors ON;");
            List<AuthorDTO>? authorDTOs = bookDTO.Authors?.ToList();
            bookDTO.Authors = new List<AuthorDTO>();

            context.Books.Add(bookDTO);
            await context.SaveChangesAsync();
            
            var result = context.Books.AsNoTracking().SingleOrDefault(b => b.ISBN == bookDTO.ISBN);

            if (authorDTOs != null) {
                foreach (AuthorDTO authorDTO in authorDTOs) {
                    var authorResult = context.Authors.AsNoTracking().SingleOrDefault(b => b.AuthorID == authorDTO.AuthorID);
                    if (result != null) {
                        if (authorResult == null) {
                            context.Authors.Add(authorDTO);
                            bookDTO.Authors.Add(authorDTO);
                        } else {
                            bookDTO.Authors.Add(authorResult);
                        }
                        context.Books.Update(bookDTO);
                        await context.SaveChangesAsync();
                    }
                }
            }
            
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors OFF;");

            transaction.Commit();
        }

        private BookDTO ToBookDTO(Book book) {
            ICollection<AuthorDTO> authors = new List<AuthorDTO>();
            foreach (Author author in book.Authors) {
                authors.Add(ToAuthorDTO(author));
            }
            BookDTO bDTO = new();
            bDTO.ISBN = book.ISBN;
            bDTO.Title = book.Title;
            bDTO.Description = book.Description;
            bDTO.Price = book.Price;
            bDTO.VAT = book.VAT;
            bDTO.Authors = authors;
            return bDTO;
        }

        private AuthorDTO ToAuthorDTO(Author author) {
            return new AuthorDTO {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname
            };
        }
    }
}
