using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class AuthorDTO {
        public AuthorDTO() {
            Books = new HashSet<BookDTO>();
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AuthorID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string AuthorSurname { get; set; }
        public virtual ICollection<BookDTO>? Books { get; set; }
    }
}