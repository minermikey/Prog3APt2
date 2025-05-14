using System.ComponentModel.DataAnnotations;

namespace Prog3A.Models
{
    public class LoginModel
    {
        // This is the primary key for the login model
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        // This is the password for the login model
        public string? Password { get; set; }
        // This will save the default role as "Farmer" 
        public string? Role { get; set; } = "User";
        public bool IsConfirmed { get; set; } = false;

    }
}
