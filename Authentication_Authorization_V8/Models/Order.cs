using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_Authorization_V8.Models
{
    [Table("Order")]
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public int IsActive { get; set; }
    }
}

