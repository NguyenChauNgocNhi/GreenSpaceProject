using System.ComponentModel.DataAnnotations;

namespace GreenSpace.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên hiển thị không được để trống")]
        [StringLength(255)]
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public DateOnly? Dob { get; set; }
    }
}