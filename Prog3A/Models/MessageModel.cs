using System;
using System.ComponentModel.DataAnnotations;

namespace Prog3A.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }  // Stores the sender's username

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }   // The message text

        public DateTime SentAt { get; set; }  // Timestamp
    }
}
