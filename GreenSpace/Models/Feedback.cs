using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name", TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("email", TypeName = "nvarchar(255)")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("message", TypeName = "nvarchar(max)")]
        [Required]
        public string Message { get; set; } = string.Empty;

        [Column("created_at", TypeName = "datetime2(6)")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
