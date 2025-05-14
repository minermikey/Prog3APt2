using Microsoft.AspNetCore.Mvc;
using Prog3A.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

// This controller is used for only validating the user login and creating cookies

namespace Prog3A.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // this is the HTTP GET, it will allow the page to be displayed 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // this si the Http Post, it will allow the user to login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Error Handling
                ModelState.AddModelError("", "Username or Password cannot be empty.");
                return View();
            }

            var user = await _context.LoginModel.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                // if user is null then it will display an error message
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            // hasing the password
            var hashedInput = PasswordHelper.HashPassword(password);
            // makes sure the password is hashed before storing it 
            if (user.Password != hashedInput)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            // If the Farmer is not confirmed, it will display a messages saying waiting for approval 
            if ((user.Role == "Farmer" || user.Role == "Employee") && !user.IsConfirmed)
            {
                ModelState.AddModelError("", "Your account is pending approval by an admin.");
                return View();
            }

            // Sign-in cookies
            var claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Name, user.Username),
                  new Claim(ClaimTypes.Role, user.Role)
               };
            // a whole bunch of claims are created and stored in the cookie
            var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
            // Redirect to the home page after successful login
            return RedirectToAction("Index", "Home");
        }

        // [Authorize(Roles = "Admin")]
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // // bring in the login moedl
        // public async Task<IActionResult> Create(LoginModel loginModel)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(loginModel);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(loginModel);
        // }

        // GET: Login/Index
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            // Gives  list of all the users and you can confirm farmers from here
            var logins = await _context.LoginModel.ToListAsync();
            return View(logins);
        }

        // Method to log out the user
        public async Task<IActionResult> Logout()
        {
            // When the user clicks log out the cookies sessions is cleared
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Login");
        }


        // This action will display all the unconfirmed farmers
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UnconfirmedUsers()
        {
            var unconfirmed = await _context.LoginModel
                .Where(u => (u.Role == "Farmer" || u.Role == "Employee") && !u.IsConfirmed)
                .ToListAsync();

            return View(unconfirmed);
        }


        // This action will ALLOW FOR confirmation of the farmer and employees, but it access is limited to the admin and employees only
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmUser(int id)
        {
            var user = await _context.LoginModel.FindAsync(id);
            if (user != null)
            {
                user.IsConfirmed = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("UnconfirmedUsers");
        }


    }
}
