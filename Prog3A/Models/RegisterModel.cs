using System.ComponentModel.DataAnnotations;

namespace Prog3A.Models
{
    public class RegisterModel
    {
        // This is the primary key for the register model
        [Key]
        public int Id { get; set; }
        // the username 
        [Required]
        public string Username { get; set; } = string.Empty;
        // the password
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        // 
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")] // compare the password with the confirm password
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";
    }
}
