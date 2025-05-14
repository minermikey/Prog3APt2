using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog3A.Models
{
    public class FarmersProducts
    {
        // thsi calss is for all the products the farmer can sell 
        [Key]
        public int Id { get; set; }
        // this is the farmer if
        public int FarmerId { get; set; }
        // product name
        public string? Name { get; set; }
        // product description
        public string? Description { get; set; }
        // product price    
        public decimal Price { get; set; }
        // product category
        public string? Category { get; set; }
        // product image
        public string? ImageUrl { get; set; }
        // this is the date the product was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; }

    }
}
