using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;
using Prog3A.Models;
using System;
using System.Threading.Tasks;

namespace Prog3A.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Message/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await _context.Messages
                                         .OrderByDescending(m => m.SentAt)
                                         .ToListAsync();
            return View(messages);
        }

        // POST: /Message/SendMessage
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage([Bind("Content")] MessageModel message)
        {
            // ModelState.Remove("UserName"); // Skip validation for username (set automatically)
            // ModelState.Remove("SentAt");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please enter a message before submitting.";
                return RedirectToAction("Index");
            }

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "You must be logged in to send a message.";
                return RedirectToAction("Index");
            }

            try
            {
                message.UserName = username;
                message.SentAt = DateTime.Now;
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Message sent successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while saving your message: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
