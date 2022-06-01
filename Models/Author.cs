using System.Collections.Generic;

namespace BookStoreP4.Models {
    public class Author {
        public int AuthorID { get; }
        public string AuthorName { get; }
        public string AuthorSurname { get; }
        public List<Book> AuthorBooks { get; }

        public Author(int authorID, string authorName, string authorSurname) {
            AuthorID = authorID;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            AuthorBooks = new List<Book>();
        }

        public Author(int authorID, string authorName, string authorSurname, List<Book> authorBooks) {
            AuthorID = authorID;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            AuthorBooks = authorBooks;
        }

        public override string ToString() {
            return $"{AuthorName} {AuthorSurname}";
        }

        public void AddBook(Book book) {
            AuthorBooks.Add(book);
        }

        public bool CheckForBook(Book book) {
            return AuthorBooks.Contains(book);
        }
    }
}
