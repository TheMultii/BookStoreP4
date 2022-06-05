using BookStoreP4.Models;
using System.Collections.Generic;

namespace BookStoreP4.ViewModels {
    public class BookViewModel : ViewModelBase {
        private readonly Book _book;

        public string BookISBN => _book.ISBN;
        public string BookTitle => _book.Title;
        public string BookDescription => _book.Description;
        public string BookDescriptionShort => $"{(_book.Description.Length > 20 ? _book.Description[..20] : _book.Description)}{(_book.Description.Length > 20 ? "..." : "")}";
        public float BookPrice => _book.Price;
        public float? BookVAT => _book.VAT;
        public string BookVATStringified => $"{(_book.VAT != null ? $"{_book.VAT*100}%" : "Zwolniony")}";
        public List<Author> BookAuthors => _book.Authors;
        public int BookAuthorsCount => _book.Authors.Count;

        public BookViewModel(Book book) {
            _book = book;
        }
    }
}
