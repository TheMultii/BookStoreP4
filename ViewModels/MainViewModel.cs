using BookStoreP4.Stores;

namespace BookStoreP4.ViewModels {
    public class MainViewModel : ViewModelBase {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged() {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
