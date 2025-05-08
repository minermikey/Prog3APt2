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

        public DbSet<LoginModel> LoginModel { get; set; }
        public DbSet<RegisterModel> RegisterModel { get; set; }

        // public DbSet<RegisterModel> RegisterModel { get; set; }

        // public DbSet<LoginViewModel> LoginViewModel { get; set; }

        // public DbSet<RegisterViewModel> RegisterViewModel { get; set; }
    }
}
