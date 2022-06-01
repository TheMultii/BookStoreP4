using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrdersCreators {
    public class DatabaseOrderCreator : IOrderCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseOrderCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task CreateOrder(Order order) {
            using (BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext()) {
                OrderDTO orderDTO = ToOrderDTO(order);

                context.Orders.Add(orderDTO);
                await context.SaveChangesAsync();
            }
        }

        private OrderDTO ToOrderDTO(Order order) {
            ICollection<OrderItemDTO> orderItems = new List<OrderItemDTO>();
            foreach (OrderItem orderItem in order.OrderItems) {
                orderItems.Add(ToOrderItemDTO(orderItem));
            }
            OrderDTO oDTO = new();
            //oDTO.OrderID = order.OrderID;
            oDTO.OrderEmployeeID = ToEmployeeDTO(order.OrderEmployee);
            oDTO.OrderCustomerID = ToCustomerDTO(order.OrderCustomer);
            oDTO.OrderDate = order.OrderDate;
            oDTO.OrderItems = orderItems;
            return oDTO;
        }

        private CustomerDTO? ToCustomerDTO(Customer orderCustomer) {
            if (orderCustomer == null) return null;
            CustomerDTO cDTO = new();
            cDTO.CustomerID = orderCustomer.CustomerID;
            cDTO.CustomerName = orderCustomer.CustomerName;
            cDTO.CustomerSurname = orderCustomer.CustomerSurname;
            cDTO.CustomerEmail = orderCustomer.CustomerEmail;
            cDTO.CustomerStreet = orderCustomer.CustomerStreet;
            cDTO.CustomerCity = orderCustomer.CustomerCity;
            cDTO.CustomerPESEL = orderCustomer.CustomerPESEL;
            return cDTO;
        }

        private EmployeeDTO? ToEmployeeDTO(Employee orderEmployee) {
            if (orderEmployee == null) return null;
            EmployeeDTO eDTO = new();
            eDTO.EmployeeID = orderEmployee.EmployeeID;
            eDTO.EmployeeName = orderEmployee.EmployeeName;
            eDTO.EmployeeSurname = orderEmployee.EmployeeSurname;
            eDTO.EmployeeEmail = orderEmployee.EmployeeEmail;
            eDTO.EmployeeStreet = orderEmployee.EmployeeStreet;
            eDTO.EmployeeCity = orderEmployee.EmployeeCity;
            eDTO.EmployeePESEL = orderEmployee.EmployeePESEL;
            return eDTO;
        }

        private OrderItemDTO ToOrderItemDTO(OrderItem orderItem) {
            OrderItemDTO orderItemDTO = new();
            orderItemDTO.OrderItemID = orderItem.OrderItemID;
            orderItemDTO.OrderItemBook = ToBookDTO(orderItem.OrderItemBook);
            orderItemDTO.Quantity = orderItem.Quantity;
            orderItemDTO.BookPrice = orderItem.BookPrice;
            orderItemDTO.BookVAT = orderItem.BookVAT;
            orderItemDTO.BookNettoValue = orderItem.BookNettoValue;
            orderItemDTO.BookBruttoValue = orderItem.BookBruttoValue;
            return orderItemDTO;
        }

        private BookDTO ToBookDTO(Book orderItemBook) {
            BookDTO bookDTO = new();
            bookDTO.ISBN = orderItemBook.ISBN;
            bookDTO.Title = orderItemBook.Title;
            bookDTO.Description = orderItemBook.Description;
            bookDTO.Price = orderItemBook.Price;
            bookDTO.VAT = orderItemBook.VAT;
            ICollection<AuthorDTO> authorDTOs = new List<AuthorDTO>();
            foreach (Author author in orderItemBook.Authors) {
                authorDTOs.Add(ToAuthorDTO(author));
            }
            return bookDTO;
        }

        private AuthorDTO ToAuthorDTO(Author author) {
            AuthorDTO authorDTO = new();
            authorDTO.AuthorID = author.AuthorID;
            authorDTO.AuthorName = author.AuthorName;
            authorDTO.AuthorSurname = author.AuthorSurname;
            return authorDTO;
        }
    }
}
