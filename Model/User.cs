using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Model
{
    public class User
    {
        [Key]
        public int UID { get; set; }  // Primary Key

        [Required]
        public string Name { get; set; }  // Required

        public string Email { get; set; }  // Unique, regex

        public string Password { get; set; }  // Regex

        [Required]
        public string Phone { get; set; }  // Required

        [Required]
        public string Role { get; set; }  // Required

        public DateTime CreatedAt { get; set; }  // DateTime

        // Relationships
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
