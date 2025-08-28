using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required, StringLength(255)]
        [Column("display_name")]
        public string? DisplayName { get; set; }

        [Required, StringLength(255)]
        [Column("username")]
        public string? Username { get; set; }

        [Required, StringLength(255)]
        [Column("password")]
        public string? Password { get; set; }

        [Required, EmailAddress, StringLength(100)]
        [Column("email")]
        public string? Email { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^\+?\d{10}$", ErrorMessage = "Số điện thoại phải có 10 chữ số.")]
        [Column("phone")]
        public string? Phone { get; set; }

        [StringLength(255)]
        [Column("address")]
        public string? Address { get; set; }

        [StringLength(255)]
        [Column("role")]
        public string? Role { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Column("dob")]
        public DateOnly? Dob { get; set; }

        [Column("create_at", TypeName = "datetime2(6)")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public void TouchCreated() => CreatedAt = DateTime.UtcNow;
    }
}


