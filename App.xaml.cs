using BookStoreP4.DBContext;
using BookStoreP4.Models;
using BookStoreP4.Services.AuthorCreators;
using BookStoreP4.Services.AuthorProviders;
using BookStoreP4.Services.BookCreators;
using BookStoreP4.Services.BooksProviders;
using BookStoreP4.Services.CustomerCreators;
using BookStoreP4.Services.CustomerProviders;
using BookStoreP4.Services.EmployeeCreators;
using BookStoreP4.Services.EmployeeProviders;
using BookStoreP4.Services.OrderItemCreators;
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
        private readonly OrderListStore _orderListStore;
        private readonly BookList _bookList;
        private readonly BookListStore _bookListStore;
        private readonly AuthorList _authorList;
        private readonly AuthorListStore _authorListStore;
        private readonly CustomerList _customerList;
        private readonly CustomerListStore _customerListStore;
        private readonly EmployeeList _employeeList;
        private readonly EmployeeListStore _employeeListStore;
        private readonly NavigationStore _navigationStore;
        private readonly string _CONNECTION_STRING_ = "Server=MARCEL-PC\\SQLEXPRESS;Database=BookStoreP4;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public App() {
            _bookStoreDBContextFactory = new(_CONNECTION_STRING_);
            IOrderProvider orderProvider = new DatabaseOrderProvider(_bookStoreDBContextFactory);
            IOrderCreator orderCreator = new DatabaseOrderCreator(_bookStoreDBContextFactory);
            IBookProvider bookProvider = new DatabaseBookProvider(_bookStoreDBContextFactory);
            IBookCreator bookCreator = new DatabaseBookCreator(_bookStoreDBContextFactory);
            IAuthorProvider authorProvider = new DatabaseAuthorProvider(_bookStoreDBContextFactory);
            IAuthorCreator authorCreator = new DatabaseAuthorCreator(_bookStoreDBContextFactory);
            ICustomerProvider customerProvider = new DatabaseCustomerProvider(_bookStoreDBContextFactory);
            ICustomerCreator customerCreator = new DatabaseCustomerCreator(_bookStoreDBContextFactory);
            IEmployeeProvider employeeProvider = new DatabaseEmployeeProvider(_bookStoreDBContextFactory);
            IEmployeeCreator employeeCreator = new DatabaseEmployeeCreator(_bookStoreDBContextFactory);
            IOrderItemCreator orderItemCreator = new DatabaseOrderItemCreator(_bookStoreDBContextFactory);
            _orderList = new(orderProvider, orderCreator, orderItemCreator);
            _orderListStore = new(_orderList);
            _bookList = new(bookProvider, bookCreator);
            _bookListStore = new(_bookList);
            _authorList = new(authorProvider, authorCreator);
            _authorListStore = new(_authorList);
            _customerList = new(customerProvider, customerCreator);
            _customerListStore = new(_customerList);
            _employeeList = new(employeeProvider, employeeCreator);
            _employeeListStore = new(_employeeList);
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

        private AddOrderViewModel CreateAddOrderViewModel() => new(_orderListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel));
        private AddOrderItemViewModel CreateAddOrderItemViewModel() => new(_orderListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel));
        private AddBookViewModel CreateAddBookViewModel() => new(_bookListStore, new Services.NavigationService(_navigationStore, CreateBookListingViewModel));
        private AddAuthorViewModel CreateAddAuthorViewModel() => new(_authorListStore, new Services.NavigationService(_navigationStore, CreateAuthorListingViewModel));
        //add customer TODO
        //add employee TODO

        private OrderListingViewModel CreateOrderListingViewModel() => OrderListingViewModel.LoadViewModel(_orderListStore, new Services.NavigationService(_navigationStore, CreateAddOrderViewModel), new Services.NavigationService(_navigationStore, CreateAddBookViewModel), new Services.NavigationService(_navigationStore, CreateBookListingViewModel), new Services.NavigationService(_navigationStore, CreateAuthorListingViewModel), new Services.NavigationService(_navigationStore, CreateCustomerListingViewModel), new Services.NavigationService(_navigationStore, CreateEmployeeListingViewModel), new Services.NavigationService(_navigationStore, CreateAddOrderItemViewModel));
        private BookListingViewModel CreateBookListingViewModel() => BookListingViewModel.LoadViewModel(_bookListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel), new Services.NavigationService(_navigationStore, CreateAddBookViewModel));
        private AuthorListingViewModel CreateAuthorListingViewModel() => AuthorListingViewModel.LoadViewModel(_authorListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel), new Services.NavigationService(_navigationStore, CreateAddAuthorViewModel));
        private CustomerListingViewModel CreateCustomerListingViewModel() => CustomerListingViewModel.LoadViewModel(_customerListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel), new Services.NavigationService(_navigationStore, CreateAddAuthorViewModel));
        private EmployeeListingViewModel CreateEmployeeListingViewModel() => EmployeeListingViewModel.LoadViewModel(_employeeListStore, new Services.NavigationService(_navigationStore, CreateOrderListingViewModel), new Services.NavigationService(_navigationStore, CreateAddAuthorViewModel));
    }
}
