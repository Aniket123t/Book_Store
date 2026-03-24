using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_Authorization_V8.Models
{
    [Table("Cart")]
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public Book Book { get; set; }

        public int IsActive { get; set; }
    }
}
