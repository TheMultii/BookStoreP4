using Bogus;
using Bogus.Extensions.Poland;
using BookStoreP4.Models;
using BookStoreP4.Services;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Commands {
    public class BogusUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string PESEL { get; set; }
        public Gender Gender { get; set; } 
    }
    
    public enum Gender {
        Male = 0,
        Female = 1
    }
    
    public class AddOrderCommand : AsyncCommandBase {
        private readonly AddOrderViewModel _addOrderViewModel;
        private readonly OrderListStore _orderListStore;
        private readonly NavigationService _orderViewNavigationService;

        public AddOrderCommand(AddOrderViewModel addOrderViewModel, OrderListStore orderListStore, NavigationService orderViewNavigationService) {
            _addOrderViewModel = addOrderViewModel;
            _orderListStore = orderListStore;
            _orderViewNavigationService = orderViewNavigationService;

            _addOrderViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter) {
            return (
                _addOrderViewModel.OrderEmployeeID != 0 &&
                _addOrderViewModel.OrderCustomerID != 0 &&
                _addOrderViewModel.OrderDateTime.Year >= DateTime.Now.Year - 1 &&
                _addOrderViewModel.OrderDateTime.Year <= DateTime.Now.Year + 1
                ) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter) {
            
            try {
                var samples = new Faker<BogusUser>("pl")
                 .StrictMode(true)
                 .RuleFor(u => u.Gender, (f, u) => f.PickRandom<Gender>())
                 .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName((Bogus.DataSets.Name.Gender)u.Gender))
                 .RuleFor(u => u.LastName, (f, u) => f.Name.LastName((Bogus.DataSets.Name.Gender)u.Gender))
                 .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                 .RuleFor(u => u.Ulica, (f, u) => f.Address.StreetAddress())
                 .RuleFor(u => u.Miasto, (f, u) => f.Address.City())
                 .RuleFor(u => u.PESEL, (f, u) => f.Person.Pesel())
                 .Generate(2);
                
                Employee e = new(_addOrderViewModel.OrderEmployeeID, samples[0].FirstName, samples[0].LastName, samples[0].Email, samples[0].Ulica, samples[0].Miasto, samples[0].PESEL);
                
                Customer p = new(_addOrderViewModel.OrderEmployeeID, samples[1].FirstName, samples[1].LastName, samples[1].Email, samples[1].Ulica, samples[1].Miasto, samples[1].PESEL);
                
                Order newOrder = new(
                    _addOrderViewModel.OrderID,
                    e,
                    p,
                    _addOrderViewModel.OrderDateTime
                );

                await _orderListStore.AddOrder(newOrder);

                _orderViewNavigationService.Navigate();
            } catch (Exception ex) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_addOrderViewModel.OrderEmployeeID) || e.PropertyName == nameof(_addOrderViewModel.OrderCustomerID) || e.PropertyName == nameof(_addOrderViewModel.OrderDateTime)) {
                OnCanExecuteChanged();
            }
        }
    }
}
