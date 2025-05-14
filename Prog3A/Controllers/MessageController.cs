using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;
using Prog3A.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prog3A.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Declaring the context for the database
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This entire code is the get method for the Index Page
        [HttpGet]
        public async Task<IActionResult> Index(string categoryFilter)
        {
            // declaring a variable to store the messages that are ordered by descending order
            var allMessages = await _context.Messages
                                            .OrderByDescending(m => m.SentAt)
                                            .ToListAsync();

            // declaring a variable to store the filtered messages. If the category filter is null or empty, it will return all messages
            var filteredMessages = string.IsNullOrEmpty(categoryFilter)
                ? allMessages
                : allMessages.Where(m => m.Category == categoryFilter).ToList();
            // If the category filter is not null or empty, it will return the messages that are filtered by the category

            /*
            
            The code below does the following :
            It extracts the categories from the messages, 
            filters out any null or empty categories,   
            and then gets the distinct categories.

            */
            ViewBag.Categories = allMessages
                .Select(m => m.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToList();
            // stores the category from the user input 
            ViewBag.SelectedCategory = categoryFilter;

            return View(filteredMessages);
        }

        // This post method to send a message
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string Content, string Category, int? ParentMessageId)
        {
            // Error handling for empty content, 
            // If there is no content, it will display an error message
            if (string.IsNullOrWhiteSpace(Content))
            {
                TempData["Error"] = "Please enter a message before submitting.";
                return RedirectToAction("Index");
            }
            // Error handling for not signing in
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "You must be logged in to send a message.";
                return RedirectToAction("Index");
            }
            // try catch for debugging purposes
            try
            {
                // saving the content into a var 
                // a var was choosen because it is a dynamic type ( Further explianation : it doesnt need to be specified ) 
                var message = new MessageModel
                {
                    UserName = username,
                    Content = Content,
                    Category = Category,
                    SentAt = DateTime.Now,
                    ParentMessageId = ParentMessageId
                };
                // adding the var message to the context
                _context.Messages.Add(message);

                await _context.SaveChangesAsync();
                // Confirmaiton that the message has been successfully sent 
                TempData["Success"] = "Message sent successfully!";

            }
            catch (Exception ex)
            {
                // Error handling for any errors that occur while saving the message
                TempData["Error"] = $"An error occurred while saving your message: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        /*
        
        Explanation for the Linking of the messages:
            when the message is first saved, it stores a null value in the ParentMessageId as ut su the parent 
            when the reply is sent the it stores the id of the parent message in the ParentMessageId

        */

        // This post method to reply to a message
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(int ParentMessageId, string Content)
        {
            // Similar to the SendMessage method, this method handles replies to messages
            var message = new MessageModel
            {
                // no category was needed here becuase its a reply to a message and 
                // will be stored in the parent message
                UserName = User.Identity.Name,
                Content = Content,
                SentAt = DateTime.Now,
                ParentMessageId = ParentMessageId
            };
            // savign to the database 
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
