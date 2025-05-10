using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;
using Prog3A.Models;
using System.Threading.Tasks;

namespace Prog3A.Controllers
{
    public class FarmersProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmersProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FarmersProducts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FarmersProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FarmersProducts product)
        {
            if (ModelState.IsValid)
            {

                // Retrieve the current user's username (or another claim that identifies the user)
                var username = User.Identity.Name;

                // Get the user from the database based on the username
                var user = await _context.LoginModel.FirstOrDefaultAsync(u => u.Username == username);

                if (user != null)
                {
                    // Assign the FarmerId from the logged-in user
                    product.FarmerId = user.Id; // Assuming your 'LoginModel' has an 'Id' field for the user
                }


                _context.FarmersProducts.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: FarmersProducts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.FarmersProducts.ToListAsync();
            return View(products);
        }
    }
}
