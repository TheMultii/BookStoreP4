using BookStoreP4.Models;
using System.Collections.Generic;

namespace BookStoreP4.ViewModels {
    public class AuthorViewModel : ViewModelBase {
        private readonly Author _author;

        public int? AuthorID => _author.AuthorID;
        public string AuthorName => _author.AuthorName;
        public string AuthorSurname => _author.AuthorSurname;
        public List<Book>? AuthorBooks => _author.AuthorBooks;
        public int AuthorBooksCount => _author.AuthorBooks.Count;

        public AuthorViewModel(Author author) {
            _author = author;
        }
    }
}
