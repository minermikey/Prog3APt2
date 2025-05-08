using System.ComponentModel.DataAnnotations;

namespace Prog3A.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "Farmer"; // This will save the default role as "Farmer" 
    }
}
