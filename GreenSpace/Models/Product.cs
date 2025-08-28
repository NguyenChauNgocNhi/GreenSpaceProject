using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public long ProductId { get; set; }

        [Required]
        [Column("category", TypeName = "nvarchar(255)")]
        public string? Category { get; set; }

        [Column("created_at", TypeName = "datetime2(6)")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("description", TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column("folder_id", TypeName = "nvarchar(255)")]
        public string? FolderId { get; set; } 

        [Required]
        [Column("name", TypeName = "nvarchar(255)")]
        public string? Name { get; set; }

        [Column("original_price")]
        public double? OriginalPrice { get; set; }

        [Required]
        [Column("price")]
        public double Price { get; set; }

        [Required, StringLength(255)]
        [Column("slug")]
        public string? Slug { get; set; }

        [Column("stock_quantity")]
        public int? StockQuantity { get; set; }


        [Column("updated_at", TypeName = "datetime2(6)")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        [NotMapped]
        public string? FirstImageUrl { get; set; }

        public void UpdateFirstImageUrl()
        {
            FirstImageUrl = Images?.FirstOrDefault()?.ImageUrl ?? "/images/no-image.jpg";
        }

        public void TouchCreated() => CreatedAt = DateTime.UtcNow;
        public void TouchUpdated() => UpdatedAt = DateTime.UtcNow;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
