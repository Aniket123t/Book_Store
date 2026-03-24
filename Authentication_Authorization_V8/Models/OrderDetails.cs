using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_Authorization_V8.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        public Book Book { get; set; }
        public Order Order { get; set; }

        public int IsActive { get; set; }
    }
}
