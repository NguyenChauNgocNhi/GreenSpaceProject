using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSpace.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required, StringLength(255)]
        [Column("name")]
        public string? Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
