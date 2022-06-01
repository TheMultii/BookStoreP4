using BookStoreP4.DBContext;
using BookStoreP4.Models;
using BookStoreP4.Services.OrdersCreators;
using BookStoreP4.Services.OrdersProviders;
using BookStoreP4.Stores;
using BookStoreP4.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace BookStoreP4 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly OrderList _orderList;
        private readonly NavigationStore _navigationStore;
        private readonly string _CONNECTION_STRING_ = "Server=MARCEL-PC\\SQLEXPRESS;Database=BookStoreP4;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public App() {
            _bookStoreDBContextFactory = new(_CONNECTION_STRING_);
            IOrderProvider orderProvider = new DatabaseOrderProvider(_bookStoreDBContextFactory);
            IOrderCreator orderCreator = new DatabaseOrderCreator(_bookStoreDBContextFactory);
            _orderList = new(orderProvider, orderCreator);
            _navigationStore = new();
        }

        protected override void OnStartup(StartupEventArgs e) {
            using (BookStoreDBContext dbContext = _bookStoreDBContextFactory.CreateDbContext()) {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateOrderListingViewModel();

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private AddOrderViewModel CreateAddOrderViewModel() => new(_orderList, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel));
        private AddBookViewModel CreateAddBookViewModel() => new(_orderList, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel));

        private OrderListingViewModel CreateOrderListingViewModel() => OrderListingViewModel.LoadViewModel(_orderList, new Services.NavigationService(_navigationStore, CreateAddOrderViewModel), new Services.NavigationService(_navigationStore, CreateAddBookViewModel));
    }
}
