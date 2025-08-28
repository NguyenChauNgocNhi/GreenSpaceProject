using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Product_image_url")]
    public class ProductImage
    {
        [Column("product_product_id", Order = 0)]
        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        [Required, StringLength(255)]
        [Column("image_url")]
        public string? ImageUrl { get; set; }
    }
}
