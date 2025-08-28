using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("title", TypeName = "nvarchar(255)")]
        public string Title { get; set; } = string.Empty;

        [Column("thumbnail_url", TypeName = "nvarchar(255)")]
        public string? ThumbnailUrl { get; set; } = string.Empty;

        [Column("content", TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        [Column("created_at", TypeName = "datetime2(6)")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
