using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.AuthorProviders {
    public interface IAuthorProvider {
        Task<IEnumerable<Author>> GetAllAuthors();
    }
}
