using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreP4.Services.OrdersCreators {
    public class DatabaseOrderCreator : IOrderCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseOrderCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task<Order> CreateOrder(Order order) {
            using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
            using var transaction = context.Database.BeginTransaction();

            if (order.OrderID == 0) {
                OrderDTO orderDTO = ToOrderDTO(order);
                EmployeeDTO? employeeDTO = orderDTO.OrderEmployeeID;
                CustomerDTO? customerDTO = orderDTO.OrderCustomerID;
                orderDTO.OrderEmployeeID = null;
                orderDTO.OrderCustomerID = null;

                context.Orders.Add(orderDTO);
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers OFF;");

                var result = context.Orders.AsNoTracking().SingleOrDefault(b => b.OrderDate == orderDTO.OrderDate);

                if (employeeDTO != null) {
                    var employeeResult = context.Employees.AsNoTracking().SingleOrDefault(b => b.EmployeeID == employeeDTO.EmployeeID);
                    if (employeeResult != null) {
                        order.OrderEmployee.EmployeeName = employeeResult.EmployeeName;
                        order.OrderEmployee.EmployeeSurname = employeeResult.EmployeeSurname;
                    }
                    if (result != null) {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Employees ON;");
                        if (employeeResult == null) {
                            context.Employees.Add(employeeDTO);
                            orderDTO.OrderEmployeeID = employeeDTO;
                        } else {
                            orderDTO.OrderEmployeeID = employeeResult;
                        }
                        context.Orders.Update(orderDTO);
                        await context.SaveChangesAsync();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Employees OFF;");
                    }
                }

                if (customerDTO != null) {
                    var customerResult = context.Customers.SingleOrDefault(b => b.CustomerID == customerDTO.CustomerID);
                    if (customerResult != null) {
                        order.OrderCustomer.CustomerName = customerResult.CustomerName;
                        order.OrderCustomer.CustomerSurname = customerResult.CustomerSurname;
                    }
                    if (result != null) {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers ON;");
                        if (customerResult == null) {
                            orderDTO.OrderCustomerID = customerDTO;
                            context.Customers.Add(customerDTO);
                        } else {
                            orderDTO.OrderCustomerID = customerResult;
                        }
                        context.Orders.Update(orderDTO);
                        await context.SaveChangesAsync();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers OFF;");
                    }
                }
                transaction.Commit();
                Order orderReturn = new(result != null ? result.OrderID : int.MaxValue, order.OrderEmployee, order.OrderCustomer, order.OrderDate);
                return result != null ? orderReturn : order;
            }
            else {
                var res = await context.Orders.Where(x => x.OrderID == order.OrderID).FirstOrDefaultAsync();
                var resE = await context.Employees.Where(x => x.EmployeeID == order.OrderEmployee.EmployeeID).FirstOrDefaultAsync();
                var resC = await context.Customers.Where(x => x.CustomerID == order.OrderCustomer.CustomerID).FirstOrDefaultAsync();
                var resOI = await context.OrderItems.Where(x => (x.OrderItemOrder == null ? -1 : x.OrderItemOrder.OrderID) == order.OrderID).Include(x => x.OrderItemBook).ToListAsync();
                res.OrderCustomerID = resC;
                res.OrderEmployeeID = resE;
                res.OrderDate = order.OrderDate;
                res.OrderItems = resOI;
                await context.SaveChangesAsync();
                transaction.Commit();
                order.OrderCustomer.CustomerName = resC.CustomerName;
                order.OrderCustomer.CustomerSurname = resC.CustomerSurname;
                order.OrderEmployee.EmployeeName = resE.EmployeeName;
                order.OrderEmployee.EmployeeSurname = resE.EmployeeSurname;
                order.OrderItems = OrderItemDTOToModel(resOI, order);
                return order;
            }
        }

        private List<OrderItem> OrderItemDTOToModel(List<OrderItemDTO> resOI, Order order) {
            List<OrderItem> orderItems = new();
            foreach (var oi in resOI) {
                orderItems.Add(new(oi.OrderItemID, order, BookDTOToModel(oi.OrderItemBook), oi.Quantity));
            }
            return orderItems;
        }
        
        private Book BookDTOToModel(BookDTO orderItemBook) {
            return new Book(orderItemBook.ISBN, orderItemBook.Title, orderItemBook.Description, new(), orderItemBook.Price, orderItemBook.VAT);
        }

        private OrderDTO ToOrderDTO(Order order) {
            ICollection<OrderItemDTO> orderItems = new List<OrderItemDTO>();
            foreach (OrderItem orderItem in order.OrderItems) {
                orderItems.Add(ToOrderItemDTO(orderItem));
            }
            OrderDTO oDTO = new();
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
