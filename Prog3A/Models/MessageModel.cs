using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog3A.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        // Sender's username
        public string UserName { get; set; }
        // The message text
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }
        // Nullable if it's not a reply
        public int? ParentMessageId { get; set; }

        [ForeignKey("ParentMessageId")]
        public MessageModel ParentMessage { get; set; }
        // Foreign key to the user who sent the message
        public string Category { get; set; } = "General";
    }
}
