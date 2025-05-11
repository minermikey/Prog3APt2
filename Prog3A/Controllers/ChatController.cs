// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Prog3A.Data;
// using Prog3A.Models;
// using System;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Prog3A.Controllers
// {
//     [Authorize]
//     public class ChatController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public ChatController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         [HttpGet]
//         public async Task<IActionResult> Index()
//         {
//             var currentUser = User.Identity.Name;

//             var messages = await _context.Messages
//                 .OrderBy(m => m.SentAt)
//                 .ToListAsync();

//             var viewModel = new ChatViewModel
//             {
//                 Messages = messages,
//                 CurrentUser = currentUser
//             };

//             return View(viewModel);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> SendMessage(ChatViewModel model)
//         {
//             var currentUser = User.Identity.Name;

//             if (!string.IsNullOrWhiteSpace(model.NewMessage))
//             {
//                 var message = new Message
//                 {
//                     UserName = currentUser,
//                     Content = model.NewMessage,
//                     SentAt = DateTime.Now
//                 };

//                 _context.Messages.Add(message);
//                 await _context.SaveChangesAsync();
//             }

//             return RedirectToAction("Index");
//         }
//     }
// }
