using CanteenManager.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CanteenManager.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Slot> Slots { get; set; }

        public DbSet<Category> Category { get; set; }

    }
}