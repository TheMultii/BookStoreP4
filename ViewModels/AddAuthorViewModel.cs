using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddAuthorViewModel : ViewModelBase {

        private string _authorName;
        public string AuthorName {
            get {
                return _authorName;
            }
            set {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        private string _authorSurname;
        public string AuthorSurname {
            get {
                return _authorSurname;
            }
            set {
                _authorSurname = value;
                OnPropertyChanged(nameof(AuthorSurname));
            }
        }

        public ICommand SubmitAuthorCommand { get; }
        public ICommand CancelCommand { get; }

        public AddAuthorViewModel(AuthorListStore authorListStore, NavigationService orderViewNavigationService) {
            SubmitAuthorCommand = new AddAuthorCommand(this, authorListStore, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
        }
    }
}
