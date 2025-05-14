using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog3A.Models
{
    public class Review
    {
        // Primary key for the review
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        // Foreign key to the product being reviewed
        [ForeignKey("ProductId")]
        public FarmersProducts Product { get; set; }
        // rating between 1 and 5
        [Range(1, 5)]
        public int Rating { get; set; }
        // stooring information 
        public string? Comment { get; set; }
        // the submission time and date 
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        // the user who submitted the review
        public string? UserId { get; set; }
    }
}
