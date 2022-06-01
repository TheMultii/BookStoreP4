using BookStoreP4.Models;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using System.Windows;

namespace BookStoreP4 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly OrderList _orderList;
        private readonly NavigationStore _navigationStore;

        public App() {
            _orderList = new();
            _navigationStore = new();
        }

        protected override void OnStartup(StartupEventArgs e) {

            _navigationStore.CurrentViewModel = CreateOrderListingViewModel();

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private AddOrderViewModel CreateAddOrderViewModel() => new(_orderList, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel));

        private OrderListingViewModel CreateOrderListingViewModel() => new(_orderList, new Services.NavigationService(_navigationStore, CreateAddOrderViewModel));
    }
}
