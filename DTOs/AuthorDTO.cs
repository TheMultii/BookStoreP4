using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class AuthorDTO {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AuthorID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string AuthorSurname { get; set; }
    }
}