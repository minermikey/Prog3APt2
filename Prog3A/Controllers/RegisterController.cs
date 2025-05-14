using Microsoft.AspNetCore.Mvc;
using Prog3A.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;

// this Controller is used for registering a new user only 

namespace Prog3A.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Declaring the context for the database
        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }
        // the get method for the register page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // the post method for the register page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // saves user as a variable for loginModel
                var user = new LoginModel
                {
                    Username = model.Username,
                    Password = PasswordHelper.HashPassword(model.Password), // Hashing the password
                    Role = model.Role
                };
                // saves data to the database
                _context.LoginModel.Add(user); // Correct DbSet
                await _context.SaveChangesAsync();
                // takes the user to the login page
                return RedirectToAction("Login", "Login");
            }
            return View(model);
        }
    }
    /*
    
    Reffrences : 
    https://www.youtube.com/watch?v=J2aoApd3mNA
    https://stackoverflow.com/questions/44715030/ef-core-migrations-fail-when-executed-in-same-upgrade
    https://stackoverflow.com/questions/77722643/how-can-i-run-the-add-migration-command

    */
}
