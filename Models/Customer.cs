namespace BookStoreP4.Models {
    public class Customer {
        public int CustomerID { get; }
        public string CustomerName { get; }
        public string CustomerSurname { get; }
        public string CustomerEmail { get; }
        public string CustomerStreet { get; }
        public string CustomerCity { get; }
        public string? CustomerPESEL { get; }

        public Customer(int customerID, string customerName, string customerSurname, string customerEmail, string customerStreet, string customerCity, string? customerPESEL = null) {
            CustomerID = customerID;
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            CustomerEmail = customerEmail;
            CustomerStreet = customerStreet;
            CustomerCity = customerCity;
            CustomerPESEL = customerPESEL;
        }

        public override string ToString() {
            return $"{CustomerName} {CustomerSurname}";
        }
    }
}
