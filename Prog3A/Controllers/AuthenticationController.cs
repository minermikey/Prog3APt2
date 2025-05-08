// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Prog3A.Models;

// namespace Prog3A.Controllers
// {
//     public class AuthenticationController : Controller
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly SignInManager<IdentityUser> _signInManager;

//         public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//         }

//         // GET: Register
//         [HttpGet]
//         public IActionResult Register()
//         {
//             return View();
//         }

//         // POST: Register
//         [HttpPost]
//         public async Task<IActionResult> Register(RegisterViewModel model)
//         {
//             if (!ModelState.IsValid)
//                 return View(model);

//             var user = new IdentityUser { UserName = model.Email, Email = model.Email };
//             var result = await _userManager.CreateAsync(user, model.Password);

//             if (result.Succeeded)
//             {
//                 await _signInManager.SignInAsync(user, isPersistent: false);
//                 return RedirectToAction("Index", "Home");
//             }

//             foreach (var error in result.Errors)
//                 ModelState.AddModelError("", error.Description);

//             return View(model);
//         }

//         // GET: Login
//         [HttpGet]
//         public IActionResult Login()
//         {
//             return View();
//         }

//         // POST: Login
//         [HttpPost]
//         public async Task<IActionResult> Login(LoginViewModel model)
//         {
//             if (!ModelState.IsValid)
//                 return View(model);

//             var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

//             if (result.Succeeded)
//                 return RedirectToAction("Index", "Home");

//             ModelState.AddModelError("", "Invalid login attempt.");
//             return View(model);
//         }

//         // POST: Logout
//         [HttpPost]
//         public async Task<IActionResult> Logout()
//         {
//             await _signInManager.SignOutAsync();
//             return RedirectToAction("Index", "Home");
//         }
//     }
// }
