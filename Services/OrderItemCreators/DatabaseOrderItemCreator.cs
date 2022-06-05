using BookStoreP4.DBContext;
using BookStoreP4.DTOs;
using BookStoreP4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreP4.Services.OrderItemCreators {
    public class DatabaseOrderItemCreator : IOrderItemCreator {
        private readonly BookStoreDBContextFactory _bookStoreDBContextFactory;

        public DatabaseOrderItemCreator(BookStoreDBContextFactory bookStoreDBContextFactory) {
            _bookStoreDBContextFactory = bookStoreDBContextFactory;
        }

        public async Task CreateOrderItem(OrderItem orderItem) {
            try {
                using BookStoreDBContext context = _bookStoreDBContextFactory.CreateDbContext();
                using var transaction = context.Database.BeginTransaction();
                OrderItemDTO orderItemDTO = ToOrderItemDTO(orderItem);
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.OrderItems ON;");

                var resultOrder = context.Orders.Where(b => b.OrderID == orderItem.OrderItemOrder.OrderID).FirstOrDefault();
                if (resultOrder != null)
                    orderItemDTO.OrderItemOrder = resultOrder;
                //var resultBook = context.Books.AsNoTracking().SingleOrDefault(b => b.ISBN == orderItem.OrderItemBook.ISBN);
                var resultBook = context.Books.Where(b => b.ISBN == orderItem.OrderItemBook.ISBN).FirstOrDefault();
                if (resultBook != null)
                    orderItemDTO.OrderItemBook = resultBook;

                int maxId = context.OrderItems.Any() ? context.OrderItems.Max(q => q.OrderItemID) + 1 : 1;
                orderItemDTO.OrderItemID = maxId;

                //orderItemDTO.OrderItemBook = resultBook;
                orderItemDTO.BookPrice = resultBook.Price;
                orderItemDTO.BookVAT = resultBook.VAT;
                orderItemDTO.BookBruttoValue = resultBook.Price;
                orderItemDTO.BookNettoValue = (float)((1 - (resultBook.VAT == null ? 0 : resultBook.VAT)) * resultBook.Price);

                context.OrderItems.Add(orderItemDTO);
                await context.SaveChangesAsync();
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.OrderItems OFF;");

                transaction.Commit();
            } catch (Exception e) {
                MessageBox.Show("Wystąpił błąd - nie udało się dodać pozycji zamówienia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e.Message);
            }
        }

        private OrderItemDTO ToOrderItemDTO(OrderItem orderItem) {
            OrderItemDTO orderItemDTO = new();
            //orderItemDTO.OrderItemID = orderItem.OrderItemID;
            //orderItemDTO.OrderItemBook = ToBookDTO(orderItem.OrderItemBook);
            orderItemDTO.Quantity = orderItem.Quantity;
            return orderItemDTO;
        }

        private BookDTO ToBookDTO(Book orderItemBook) {
            BookDTO bookDTO = new();
            bookDTO.ISBN = orderItemBook.ISBN;
            return bookDTO;
        }
    }
}
