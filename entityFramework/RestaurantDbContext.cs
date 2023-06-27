using Microsoft.EntityFrameworkCore;

namespace API.entityFramework
{
    public class RestaurantDbContext: DbContext
    {
        public DbSet<TestApiClassEntityFramework> Type { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<RestaurantDTO>()
                .Property(n => n.Name).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<DishDTO>()
                .Property(n => n.Name).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Address>()
                .Property(n => n.City).HasMaxLength(50);
           
            modelBuilder.Entity<Address>()
                .Property(n => n.Street).HasMaxLength(50);

        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionUrl);
        }
    }
}
