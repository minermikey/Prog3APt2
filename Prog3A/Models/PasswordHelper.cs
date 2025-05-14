using System.Security.Cryptography;
using System.Text;

namespace Prog3A.Models
{
    public static class PasswordHelper
    {

        /*
        
            Reffrences : 
            https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256?view=net-9.0 
            https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256 
        
        */
        public static string HashPassword(string password)
        {
            // this creats an instance of the SHA256 class
            using var sha256 = SHA256.Create();
            // turning the password into a byte
            var bytes = Encoding.UTF8.GetBytes(password);
            // converts the byte to a hash
            var hash = sha256.ComputeHash(bytes);
            // converts the hash to a string
            return Convert.ToBase64String(hash);

        }
    }
}