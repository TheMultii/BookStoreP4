using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class OrderDTO {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderID { get; set; }
        public virtual EmployeeDTO? OrderEmployeeID { get; set; }
        public virtual CustomerDTO? OrderCustomerID { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
