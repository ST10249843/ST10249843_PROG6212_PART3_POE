using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user

        // Email field with required validation and email format validation
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        // Name field with required validation
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        // Surname field with required validation
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        // Role field with required validation
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        // Password field with required validation and password type
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
