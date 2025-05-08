using Microsoft.AspNetCore.Mvc;
using Prog3A.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;

namespace Prog3A.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username or Password cannot be empty.");
                return View();
            }

            // Look up the user by username in the LoginModel table
            var user = await _context.LoginModel
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            // Hash the input password and compare it to the stored hash
            var hashedInput = PasswordHelper.HashPassword(password);

            if (user.Password == hashedInput)
            {
                TempData["Username"] = user.Username;
                TempData["Role"] = user.Role;

                return RedirectToAction("Index"); // Or whatever your protected page is
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }





        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginModel);
        }

        // GET: Login/Index
        public async Task<IActionResult> Index()
        {
            var logins = await _context.LoginModel.ToListAsync();
            return View(logins);
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            ViewBag.Username = TempData["Username"];
            ViewBag.Role = TempData["Role"];
            return View();
        }
    }
}
