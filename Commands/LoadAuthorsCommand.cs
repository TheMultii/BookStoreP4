using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class LoadAuthorsCommand : AsyncCommandBase {
        private readonly AuthorListingViewModel _viewModel;
        private readonly AuthorListStore _authorListStore;

        public LoadAuthorsCommand(AuthorListingViewModel viewModel, AuthorListStore authorListStore) {
            _viewModel = viewModel;
            _authorListStore = authorListStore;
        }

        public override async Task ExecuteAsync(object parameter) {
            _viewModel.IsLoading = true;
            try {
                await _authorListStore.Load();
                _viewModel.UpdateAuthor(_authorListStore.Authors);
            } catch(Exception) {
                MessageBox.Show("Wystąpił błąd - nie udało się wyświetlić autorów", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                _viewModel.IsLoading = false;
            }
        }
    }
}
