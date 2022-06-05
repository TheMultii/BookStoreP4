using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class BookListingViewModel : ViewModelBase {

        private readonly ObservableCollection<BookViewModel> _books;

        public IEnumerable<BookViewModel> Books => _books;

        private bool _isLoading;
        public bool IsLoading {
            get {
                return _isLoading;
            }
            set {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadBooksCommand { get; }
        public ICommand ListOrderCommand { get; }
        public ICommand AddBookCommand { get; }

        public BookListingViewModel(BookListStore bookListStore, NavigationService orderViewNavigationService, NavigationService bookAddNavigationService) {
            _books = new();
            LoadBooksCommand = new LoadBooksCommand(this, bookListStore);
            ListOrderCommand = new NavigateCommand(orderViewNavigationService);
            AddBookCommand = new NavigateCommand(bookAddNavigationService);
        }

        public static BookListingViewModel LoadViewModel(BookListStore bookListStore, NavigationService orderViewNavigationService, NavigationService bookAddNavigationService) {
            BookListingViewModel viewModel = new(bookListStore, orderViewNavigationService, bookAddNavigationService);
            viewModel.LoadBooksCommand.Execute(null);
            return viewModel;
        }

        public void UpdateBooks(IEnumerable<Book> books) {
            _books.Clear();
            foreach (Book _book in books) {
                BookViewModel bookViewModel = new(_book);
                _books.Add(bookViewModel);
            }
        }
    }
}
