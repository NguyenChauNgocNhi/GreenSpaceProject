using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Order_items")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("name"), StringLength(255)]
        public string? Name { get; set; }

        [Column("price")]
        public double? Price { get; set; }
        
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("image_url"), StringLength(255)]
        public string? ImageUrl { get; set; }

        [Column("order_id")]
        public long OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
