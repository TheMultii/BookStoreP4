using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class CustomerDTO {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerStreet { get; set; }
        [Required]
        public string CustomerCity { get; set; }
        public string? CustomerPESEL { get; set; }
    }
}
