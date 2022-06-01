using BookStoreP4.ViewModels;
using System;

namespace BookStoreP4.Stores {
    public class NavigationStore {
        private ViewModelBase _currentViewModel;
        
        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged() {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }
}
