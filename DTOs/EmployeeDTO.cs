using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class EmployeeDTO {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeSurname { get; set; }
        [Required]
        public string EmployeeEmail { get; set; }
        [Required]
        public string EmployeeStreet { get; set; }
        [Required]
        public string EmployeeCity { get; set; }
        public string? EmployeePESEL { get; set; }
    }
}
