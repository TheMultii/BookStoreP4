using BookStoreP4.Commands;
using BookStoreP4.Stores;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class MainViewModel : ViewModelBase {
        private readonly NavigationStore _navigationStore;
        public ICommand CloseAppCommand { get; }
        public ICommand MinimizeAppCommand { get; }
        public ICommand MaximizeAppCommand { get; }
        public ICommand ShowAuthorModalCommand { get; }
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            CloseAppCommand = new CloseAppCommand();
            MinimizeAppCommand = new MinimizeAppCommand();
            MaximizeAppCommand = new MaximizeAppCommand();
            ShowAuthorModalCommand = new ShowAuthorModalCommand();
        }

        private void OnCurrentViewModelChanged() {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
