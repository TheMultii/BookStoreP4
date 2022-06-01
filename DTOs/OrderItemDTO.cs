using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class OrderItemDTO {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderItemID { get; set; }
        [Required]
        public BookDTO OrderItemBook { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float BookPrice { get; set; }
        public float? BookVAT { get; set; }
        [Required]
        public float BookNettoValue { get; set; }
        [Required]
        public float BookBruttoValue { get; set; }
    }
}
