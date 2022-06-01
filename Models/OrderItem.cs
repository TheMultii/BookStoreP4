namespace BookStoreP4.Models {
    public class OrderItem {
        public int OrderItemID { get; }
        public Order OrderItemOrder { get; }
        public Book OrderItemBook { get; }
        public int Quantity { get; }
        public decimal BookPrice { get; }
        public decimal BookVAT { get; }
        public decimal BookNettoValue { get; }
        public decimal BookBruttoValue { get; }

        public OrderItem(int orderItemID, Order orderItemOrder, Book book, int quantity) {
            OrderItemID = orderItemID;
            OrderItemOrder = orderItemOrder;
            OrderItemBook = book;
            Quantity = quantity;
            BookPrice = BookNettoValue = book.Price;
            BookVAT = book.VAT;
            BookBruttoValue = (1 + book.VAT) * book.Price;
        }
    }
}
