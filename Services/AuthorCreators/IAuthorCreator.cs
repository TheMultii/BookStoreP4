using BookStoreP4.Models;
using System.Threading.Tasks;

namespace BookStoreP4.Services.AuthorCreators {
    public interface IAuthorCreator {
        Task<Author> CreateAuthor(Author author);
    }
}
