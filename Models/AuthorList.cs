using BookStoreP4.Services.AuthorCreators;
using BookStoreP4.Services.AuthorProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Models {
    public class AuthorList {
        private readonly IAuthorProvider _authorProvider;
        private readonly IAuthorCreator _authorCreator;

        public AuthorList(IAuthorProvider authorProvider, IAuthorCreator authorCreator) {
            _authorProvider = authorProvider;
            _authorCreator = authorCreator;
        }
        
        public async Task<IEnumerable<Author>> GetAuthors() => await _authorProvider.GetAllAuthors();

        public async Task<Author> AddAuthor(Author author) {
            return await _authorCreator.CreateAuthor(author);
        }
    }
}
