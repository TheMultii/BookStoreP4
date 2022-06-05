using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadBooksCommand : AsyncCommandBase {
        private readonly BookListingViewModel _viewModel;
        private readonly BookListStore _bookListStore;

        public LoadBooksCommand(BookListingViewModel viewModel, BookListStore bookListStore) {
            _viewModel = viewModel;
            _bookListStore = bookListStore;
        }

        public override async Task ExecuteAsync(object parameter) {
            _viewModel.IsLoading = true;
            try {
                await _bookListStore.Load();
                _viewModel.UpdateBooks(_bookListStore.Books);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się wyświetlić książek", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                _viewModel.IsLoading = false;
            }
        }
    }
}
