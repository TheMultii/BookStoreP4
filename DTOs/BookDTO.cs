using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreP4.DTOs {
    public class BookDTO {
        public BookDTO() {
            Authors = new HashSet<AuthorDTO>();
        }
        
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<AuthorDTO>? Authors { get; set; }
        [Required]
        public float Price { get; set; }
        public float? VAT { get; set; }
    }
}