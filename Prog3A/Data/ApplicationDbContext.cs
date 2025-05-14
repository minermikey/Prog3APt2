using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prog3A.Models;

namespace Prog3A.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /*

            Declarign the classes that will be used as tables in the database

            1) Used for the login page
            2) Used for the register page
            3) Used for the farmers products page and pages related to everything Farmer such as Shop 
            4) Used for the messages page and pages related to everything Messages
            5) Used for the reviews page

        */
        //1 
        public DbSet<LoginModel> LoginModel { get; set; }
        //2
        public DbSet<RegisterModel> RegisterModel { get; set; }
        //3
        public DbSet<FarmersProducts> FarmersProducts { get; set; }
        //4
        public DbSet<MessageModel> Messages { get; set; }
        //5
        public DbSet<Review> Reviews { get; set; }

    }
}
