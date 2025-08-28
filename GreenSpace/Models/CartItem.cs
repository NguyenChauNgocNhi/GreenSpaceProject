using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Cart_item")]                     
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("image_url", TypeName = "nvarchar(255)")]
        public string? ImageUrl { get; set; }

        [Column("name", TypeName = "nvarchar(255)")]
        public string? Name { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("product_id")]
        public long? ProductId { get; set; }  

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("slug", TypeName = "nvarchar(255)")]
        public string? Slug { get; set; }

        [Column("username", TypeName = "nvarchar(255)")]
        public string? Username { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
