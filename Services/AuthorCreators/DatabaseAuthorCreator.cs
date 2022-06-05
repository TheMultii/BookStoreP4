using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Services.AuthorCreators {
    public class DatabaseAuthorCreator : IAuthorCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseAuthorCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }
        
        public async Task<Author> CreateAuthor(Author author) {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            using var transaction = context.Database.BeginTransaction();
            AuthorDTO authorDTO = ToAuthorDTO(author);

            context.Authors.Add(authorDTO);
            await context.SaveChangesAsync();

            var result = context.Authors.AsNoTracking().SingleOrDefault(b => b.AuthorName == author.AuthorName && b.AuthorSurname == author.AuthorSurname);

            Author authorReturn = new(result != null ? result.AuthorID : int.MaxValue, result?.AuthorName ?? author.AuthorName, result?.AuthorSurname ?? author.AuthorSurname);
            
            transaction.Commit();
            return result != null ? authorReturn : author;
        }

        private AuthorDTO ToAuthorDTO(Author author) {
            AuthorDTO authorDTO = new();
            authorDTO.AuthorName = author.AuthorName;
            authorDTO.AuthorSurname = author.AuthorSurname;
            return authorDTO;
        }
    }
}
