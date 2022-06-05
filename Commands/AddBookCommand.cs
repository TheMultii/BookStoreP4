using BookStoreP4.DBContext;
using BookStoreP4.Models;
using Bogus;
using Bogus.Extensions.Poland;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class AddBookCommand : AsyncCommandBase {
        private readonly AddBookViewModel _addBookViewModel;
        private readonly BookListStore _bookListStore;
        private readonly NavigationService _orderViewNavigationService;

        public AddBookCommand(AddBookViewModel addBookViewModel, BookListStore bookListStore, NavigationService orderViewNavigationService) {
            _addBookViewModel = addBookViewModel;
            _bookListStore = bookListStore;
            _orderViewNavigationService = orderViewNavigationService;

            _addBookViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addBookViewModel?.BookTitle != "" && _addBookViewModel?.BookTitle?.Length > 2 &&
                _addBookViewModel?.BookDescription != "" && _addBookViewModel?.BookDescription?.Length > 2 &&
                float.Parse((_addBookViewModel?.BookPrice) ?? "0") > 0 && _addBookViewModel?.BookISBN?.Length == 13
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            try {
                List<Author> authors = new();
                if(_addBookViewModel?.AuthorIDs != null) {
                    string[]? arr = _addBookViewModel?.AuthorIDs?.Trim()?.Split(",");
                    int count = arr?.Length ?? 0;
                    var samples = new Faker<BogusUser>("pl")
                     .StrictMode(true)
                     .RuleFor(u => u.Gender, (f, u) => f.PickRandom<Gender>())
                     .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName((Bogus.DataSets.Name.Gender)u.Gender))
                     .RuleFor(u => u.LastName, (f, u) => f.Name.LastName((Bogus.DataSets.Name.Gender)u.Gender))
                     .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                     .RuleFor(u => u.Ulica, (f, u) => f.Address.StreetAddress())
                     .RuleFor(u => u.Miasto, (f, u) => f.Address.City())
                     .RuleFor(u => u.PESEL, (f, u) => f.Person.Pesel())
                     .Generate(count);
                    foreach (var a in arr) {
                        Author author = new(int.Parse(a), samples[0].FirstName, samples[0].LastName);
                        authors.Add(author);
                    }                    
                }
                Book newBook = new(_addBookViewModel.BookISBN, _addBookViewModel.BookTitle, _addBookViewModel.BookDescription, authors, float.Parse(_addBookViewModel?.BookPrice ?? "0"), _addBookViewModel?.BookVAT != null ? float.Parse(_addBookViewModel?.BookVAT ?? "0") : null);

                await _bookListStore.AddBook(newBook);

                _orderViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać pozycji", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addBookViewModel.BookTitle) || e.PropertyName == nameof(_addBookViewModel.BookDescription) || e.PropertyName == nameof(_addBookViewModel.BookPrice)) {
                OnCanExecuteChanged();
            }
        }
    }
}
