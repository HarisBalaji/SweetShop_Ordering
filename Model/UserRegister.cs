using System.ComponentModel.DataAnnotations;

namespace SweetShop.Model
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters long")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
