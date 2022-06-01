using System.Collections.Generic;

namespace BookStoreP4.Models {
    public class Book {
        public string ISBN { get; }
        public string Title { get; }
        public string Description { get; }
        public List<Author> Authors { get; }
        public float Price { get; }
        public float VAT { get; }

        public Book(string isbn, string title, string description, List<Author> authors, float price, float vat) {
            ISBN = isbn;
            Title = title;
            Description = description;
            Authors = authors;
            Price = price;
            VAT = vat;
        }

        public bool CheckForAuthor(string authorName, string authorSurname) {
            foreach (Author author in Authors) {
                if (author.AuthorName == authorName && author.AuthorSurname == authorSurname) {
                    return true;
                }
            }
            return false;
        }

        public bool CheckForAuthor(Author author) {
            return Authors.Contains(author);
        }
    }
}
