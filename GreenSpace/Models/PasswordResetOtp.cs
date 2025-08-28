using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Password_reset_otp")]
    public class PasswordResetOtp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [Required, StringLength(10)]
        public string? Otp { get; set; }

        [Column("expires_at", TypeName = "datetime2(6)")]
        public DateTime ExpiresAt { get; set; }

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        [Column("used_at", TypeName = "datetime2(6)")]
        public DateTime? UsedAt { get; set; }
    }
}
