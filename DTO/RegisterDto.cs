using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }

        [EmailAddress]  // Built-in validation for email format
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one number.")]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
