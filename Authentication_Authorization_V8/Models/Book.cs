using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_Authorization_V8.Models
{
    [Table("Book")]
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }

        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public int IsActive { get; set; }
    }
}
