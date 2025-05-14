using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;
using Prog3A.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace Prog3A.Controllers
{
    public class FarmersProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // connecting the controller to the database
        public FarmersProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get Method for the Create Page
        // To display the Page
        [Authorize(Roles = "Farmer,Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            // Returnign the Page View
            return View();
        }

        //Post Method for the Create Page
        // only open to Farmers and Admins
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FarmersProducts product, IFormFile imageFile)
        {
            // Check if the model state is valid
            // getting ther name of the user
            var username = User.Identity != null ? User.Identity.Name : null;
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("", "You must be logged in to create a product.");
                return View(product);
            }

            // finding the user in the Login Model database
            var user = await _context.LoginModel.FirstOrDefaultAsync(u => u.Username == username);
            // Extra Error Handling
            // If the user is not null then assign the farmer id as the user id
            if (user != null)
            {
                product.FarmerId = user.Id;
            }

            try
            {

                //Reffrences for this section 
                /*

                https://stackoverflow.com/questions/17944645/how-save-uploaded-file-c-sharp-mvc
                https://help.syncfusion.com/aspnetmvc/uploadbox/file-actions
                https://help.syncfusion.com/aspnetmvc/uploadbox/restricting-uploading-files-based-on-its-extension
                https://www.youtube.com/watch?v=J2aoApd3mNA
                https://medium.com/@paul.pietzko/uploading-and-storing-files-with-asp-net-and-wwwroot-1efc23d34ee5

                */

                // Checking if the Image Field is populated and the file is not empty
                if (imageFile != null && imageFile.Length > 0)
                {
                    // This is used for validating the Image Type. It ensure that its either - .jpg", ".jpeg", ".png", ".gif" - to ensure that no 
                    // farmers put in any other file types
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    // Saving the path of the file in lower case
                    var extension = Path.GetExtension(imageFile.FileName).ToLower();
                    // Error handlign to check that : if the extenstion is not either previously started it will though an error
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("Image", "Only image files (JPG, PNG, GIF) are allowed.");
                        return View(product);
                    }

                    // Create the uploads folder if it doesn't exist
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
                    // If the file doesnt exist just create one 
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name by using a Globally Unique Identifie and the extension so the image is stored in the same format 
                    var uniqueFileName = Guid.NewGuid().ToString() + extension;

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Saving the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Saving the path to the imafe in ..... + the unique file name. this will ensure that no 2 images name the same will clash
                    product.ImageUrl = "/images/products/" + uniqueFileName;
                }
            }
            catch (Exception ex)
            {
                // Some silly Error Handling
                ModelState.AddModelError("", "An error occurred while uploading the image: " + ex.Message);
                return View(product);
            }

            // Savign to the database
            _context.FarmersProducts.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        // This controller can onyl be viewed by Admins
        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> Index(int? farmerId, DateTime? startDate, DateTime? endDate)
        {
            // this create a query object
            var query = _context.FarmersProducts.AsQueryable();
            // this is used to filter the products based on the farmer id
            if (farmerId.HasValue)
            {
                query = query.Where(p => p.FarmerId == farmerId.Value);
            }
            // This is used to filter the products based on the date range
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= startDate.Value && p.CreatedAt <= endDate.Value);
            }
            // stores the products in a list and then basically sends it to be displayed 
            var products = await query.ToListAsync();
            return View(products);
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Shop()
        {
            // This is used to get all the products from the database
            var products = await _context.FarmersProducts
                .Include(p => p.Reviews)
                .ToListAsync();

            return View(products); // Send to Shop.cshtml
        }

        // To leave a review you have to be an authorized user
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LeaveReview(int productId)
        {
            // This is used to get the product from the database by using the product id
            var product = await _context.FarmersProducts.FindAsync(productId);
            if (product == null) return NotFound();
            // This is used to get the reviews from the database by using the product id
            ViewBag.ProductName = product.Name;
            ViewBag.ProductId = product.Id;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveReview(Review review)
        {
            // This saves  the users username for use in the method 
            var username = User.Identity.Name;
            var user = await _context.LoginModel.FirstOrDefaultAsync(u => u.Username == username);
            // If the user is not found then it will throw an error ( EXTRA ERROR HANDLING)
            if (user == null)
            {
                ModelState.AddModelError("", "User not found or not logged in.");
                return View(review);
            }
            // saving the reviews id as the loged in users id
            string userId = user.Id.ToString();
            review.UserId = userId;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            // Vonfirmation teh Review was submitted
            TempData["Success"] = "Review submitted successfully!";
            return RedirectToAction("Shop");
        }







    }
}