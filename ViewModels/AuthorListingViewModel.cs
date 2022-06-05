using BookStoreP4.Commands;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AuthorListingViewModel : ViewModelBase {

        private readonly ObservableCollection<AuthorViewModel> _authors;

        public IEnumerable<AuthorViewModel> Authors => _authors;

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
        
        public ICommand LoadAuthorsCommand { get; }
        public ICommand ListOrderCommand { get; }
        public ICommand AddAuthorCommand { get; }

        public AuthorListingViewModel(AuthorListStore authorListStore, NavigationService orderViewNavigationService, NavigationService addAuthorNavigationService) {
            _authors = new();
            LoadAuthorsCommand = new LoadAuthorsCommand(this, authorListStore);
            ListOrderCommand = new NavigateCommand(orderViewNavigationService);
            AddAuthorCommand = new NavigateCommand(addAuthorNavigationService);
        }

        public static AuthorListingViewModel LoadViewModel(AuthorListStore authorListStore, NavigationService orderViewNavigationService, NavigationService addAuthorNavigationService) {
            AuthorListingViewModel viewModel = new(authorListStore, orderViewNavigationService, addAuthorNavigationService);
            viewModel.LoadAuthorsCommand.Execute(null);
            return viewModel;
        }
        
        public void UpdateAuthor(IEnumerable<Author> authors) {
            _authors.Clear();
            foreach (Author _author in authors) {
                AuthorViewModel authorViewModel = new(_author);
                _authors.Add(authorViewModel);
            }
        }
    }
}
