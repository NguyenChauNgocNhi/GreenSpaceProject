using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("order_code", TypeName = "nvarchar(255)")]
        public string OrderCode { get; set; } = $"ORD{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";

        [Required]
        [Column("username", TypeName = "nvarchar(255)")]
        public string? Username { get; set; }

        [Required]
        [Column("email", TypeName = "nvarchar(255)")]
        public string? Email { get; set; }

        [Required]
        [Column("full_name", TypeName = "nvarchar(255)")]
        public string? FullName { get; set; }

        [Column("phone", TypeName = "nvarchar(255)")]
        public string? Phone { get; set; }

        [Column("address", TypeName = "nvarchar(255)")]
        public string? Address { get; set; }

        [Column("shipping_fee")]
        public double? ShippingFee { get; set; }

        [Column("total_amount")]
        public double? TotalAmount { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column("created_at", TypeName = "datetime2(6)")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
