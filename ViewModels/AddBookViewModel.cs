using BookStoreP4.Commands;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BookStoreP4.ViewModels {
    public class AddBookViewModel : ViewModelBase {

        private string _BookISBN;
        public string BookISBN {
            get {
                return _BookISBN;
            }
            set {
                if (value.Length > 0 && value.All(char.IsDigit)) {
                    _BookISBN = value;
                    OnPropertyChanged(nameof(BookISBN));
                }
            }
        }

        private string _BookTitle;
        public string BookTitle {
            get {
                return _BookTitle;
            }
            set {
                _BookTitle = value;
                OnPropertyChanged(nameof(BookTitle));
            }
        }

        private string _BookDescription;
        public string BookDescription {
            get {
                return _BookDescription;
            }
            set {
                _BookDescription = value;
                OnPropertyChanged(nameof(BookDescription));
            }
        }

        private string _authorIDs;
        public string AuthorIDs {
            get {
                return _authorIDs;
            }
            set {
                string toSet = "";
                value = value.Replace(".", ",");
                foreach (var splitted in value.Trim().Split(",")) {
                    string temp = splitted.Trim();
                    if (splitted.Length > 0 && int.TryParse(splitted, out int i) && i > 0) {
                        toSet += splitted + ",";
                    }
                }

                if (toSet.Length > 0 && toSet[toSet.Length - 1] == ',') {
                    toSet = toSet.Remove(toSet.Length - 1);
                }
                _authorIDs = toSet;
            }
        }

        internal float _BookPrice;
        public string BookPrice {
            get {
                return _BookPrice.ToString();
            }
            set {
                value = value.Replace(".", ",");
                float f_value = float.Parse(value);
                if (f_value > 0) {
                    _BookPrice = f_value;
                    OnPropertyChanged(nameof(BookPrice));
                }
            }
        }

        private float? _BookVAT;
        public string? BookVAT {
            get {
                return _BookVAT.ToString();
            }
            set {
                value = value == null ? "" : value.Replace(".", ",");
                float? f_value;
                try {
                    f_value = float.Parse(string.Format("{0:0.00}", float.Parse(value)));
                } catch (Exception) {
                    f_value = null;
                }
                if ((f_value >= 0 && f_value <= 1) || f_value == null) {
                    _BookVAT = f_value;
                    OnPropertyChanged(nameof(BookVAT));
                }
            }
        }
        
        public ICommand SubmitBookCommand { get; }
        public ICommand CancelCommand { get; }

        public AddBookViewModel(BookListStore bookListStore, NavigationService orderViewNavigationService) {
            SubmitBookCommand = new AddBookCommand(this, bookListStore, orderViewNavigationService);
            CancelCommand = new NavigateCommand(orderViewNavigationService);
        }
    }
}
