using BookStoreP4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Stores {
    public class AuthorListStore {
        private readonly AuthorList _authorList;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Author> _authors;

        public IEnumerable<Author> Authors => _authors;

        public AuthorListStore(AuthorList authorList) {
            _authorList = authorList;
            _initializeLazy = new(Initialize);
            _authors = new();
        }
        public async Task Load() {
            await _initializeLazy.Value;
        }
        
        public async Task AddAuthor(Author newAuthor) {
            Author added = await _authorList.AddAuthor(newAuthor);
            _authors.Add(added);
        }

        private async Task Initialize() {
            IEnumerable<Author> authors = await _authorList.GetAuthors();
            _authors.Clear();
            _authors.AddRange(authors);
        }
    }
}
