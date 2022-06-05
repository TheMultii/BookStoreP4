using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddAuthorCommand : AsyncCommandBase {
        private readonly AddAuthorViewModel _addAuthorViewModel;
        private readonly AuthorListStore _authorListStore;
        private readonly NavigationService _orderViewNavigationService;

        public AddAuthorCommand(AddAuthorViewModel addAuthorViewModel, AuthorListStore authorListStore, NavigationService orderViewNavigationService) {
            _addAuthorViewModel = addAuthorViewModel;
            _authorListStore = authorListStore;
            _orderViewNavigationService = orderViewNavigationService;

            _addAuthorViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addAuthorViewModel?.AuthorName != "" && _addAuthorViewModel?.AuthorName?.Length <= 50 &&
                _addAuthorViewModel?.AuthorSurname != "" && _addAuthorViewModel?.AuthorSurname?.Length <= 55
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            try {
                Author newAuthor = new(int.MaxValue, _addAuthorViewModel.AuthorName, _addAuthorViewModel.AuthorSurname);

                await _authorListStore.AddAuthor(newAuthor);

                _orderViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać autora", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addAuthorViewModel.AuthorName) || e.PropertyName == nameof(_addAuthorViewModel.AuthorSurname)) {
                OnCanExecuteChanged();
            }
        }
    }
}
