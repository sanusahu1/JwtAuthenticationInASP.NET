using Microsoft.EntityFrameworkCore;
using JwtAuthenticationDemo.Models;

namespace JwtAuthenticationDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
