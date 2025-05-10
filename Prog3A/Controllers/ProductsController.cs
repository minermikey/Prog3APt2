// using Prog3A.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Prog3A.Data;

// namespace Prog3A.Controllers
// {
//     public class ProductsController : Controller
//     {

//         private readonly ApplicationDbContext _context;

//         public ProductsController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         [HttpGet]
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // [Authorize(Roles = "Farmer")]
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(ProductsModel product)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View(product); // Redisplay form with validation errors
//             }

//             _context.ProductsModel.Add(product);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Index");
//         }

//         [HttpGet]
//         public async Task<IActionResult> Index()
//         {
//             var products = await _context.ProductsModel.ToListAsync();
//             return View(products);
//         }



//     }
// }