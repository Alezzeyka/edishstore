using Microsoft.EntityFrameworkCore;
using ASPLab.Data.Models;

namespace ASPLab.Data.DB.Context
{
    public class AppDBContent: DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        { }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ShopCartItem> ShopCartItems { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<UserRoles> UserRole { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("OrderNumbers")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<Order>()
                .Property(order => order.OrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");
            modelBuilder.Entity<User>()
                .HasMany(u=>u.Orders)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrgerDetails)
                .WithOne(od => od.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
