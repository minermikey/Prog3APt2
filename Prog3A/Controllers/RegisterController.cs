using Microsoft.AspNetCore.Mvc;
using Prog3A.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;

namespace Prog3A.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new LoginModel
                {
                    Username = model.Username,
                    Password = PasswordHelper.HashPassword(model.Password),
                    Role = model.Role
                };

                _context.LoginModel.Add(user); // Correct DbSet
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Login");
            }

            return View(model);
        }




    }
}
